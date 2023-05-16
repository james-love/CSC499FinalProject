using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private const float Threshold = 0.01f;
    private readonly float terminalVelocity = 53.0f;
    [SerializeField] private PlayerMovementInputs input;
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    [SerializeField] private float moveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    [SerializeField] private float sprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    [SerializeField] private float rotationSmoothTime = 0.12f;
    [Tooltip("Rotation speed of the character in 1st person")]
    [SerializeField] private float rotationSpeed = 1.0f;

    [Tooltip("Acceleration and deceleration")]
    [SerializeField] private float speedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    [SerializeField] private float jumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    [SerializeField] private float gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    [SerializeField] private float jumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    [SerializeField] private float fallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    [SerializeField] private bool grounded = true;

    [Tooltip("Useful for rough ground")]
    [SerializeField] private float groundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    [SerializeField] private float groundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    [SerializeField] private LayerMask groundLayers;

    [Header("Cinemachine")]
    [SerializeField] private GameObject cameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    [SerializeField] private float topClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    [SerializeField] private float bottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    [SerializeField] private float cameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    [SerializeField] private bool lockCameraPosition = false;

    // cinemachine
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;

    // player
    private float speed;
    private float animationBlend;
    private float targetRotation = 0.0f;
    private float rotationVelocity;
    private float verticalVelocity;
    [SerializeField] private float slideForceMultiplier = 3f;
    private Vector3 slidingForce = Vector3.zero;

    // timeout deltatime
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;

    private Animator animator;
    private CharacterController controller;
    private GameObject mainCamera;

    // for resetting player position
    private Vector3 lastSafeGroundedPosition;
    [SerializeField] private float safegroundCheckFrequency = 1f;
    private float timeSinceLastSafeGroundCheck;

    // counter to keep track of how many tomes have been collected
    public int TomesCollected { get; set; } = 0;

    public void Teleport(Vector3 newPosition)
    {
        transform.position = newPosition;
        Physics.SyncTransforms();
    }

    public void ResetPos()
    {
        LevelManager.Instance.Pause();
        Teleport(lastSafeGroundedPosition);
        LevelManager.Instance.Resume();
    }

    // function to update the tome counter (connected to Tome and UIManager scripts)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tome"))
        {
            TomesCollected += 1;
            UIManager.Instance.updateCounter(TomesCollected);
            Destroy(other.gameObject);
        }
    }

    private void Awake()
    {
        // get a reference to our main camera
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        timeSinceLastSafeGroundCheck = safegroundCheckFrequency;
    }

    private void Start()
    {
        cinemachineTargetYaw = cameraTarget.transform.rotation.eulerAngles.y;

        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();

        // reset our timeouts on start
        jumpTimeoutDelta = jumpTimeout;
        fallTimeoutDelta = fallTimeout;
    }

    private void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        CalculateSlidingForce();
        SafeGroundCheck();
        Move();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

        animator.SetBool("Grounded", grounded);
    }

    private void CalculateSlidingForce()
    {
        if (grounded && Physics.SphereCast(new(transform.position.x, transform.position.y + groundedRadius + 0.05f, transform.position.z), groundedRadius, Vector3.down, out var hit, 0.1f, groundLayers, QueryTriggerInteraction.Ignore))
            slidingForce = Vector3.Angle(Vector3.up, hit.normal) > controller.slopeLimit ? new((1f - hit.normal.y) * hit.normal.x * slideForceMultiplier, 0f, (1f - hit.normal.y) * hit.normal.z * slideForceMultiplier) : Vector3.zero;
    }

    private void SafeGroundCheck()
    {
        timeSinceLastSafeGroundCheck = Mathf.Clamp(timeSinceLastSafeGroundCheck + Time.deltaTime, 0f, safegroundCheckFrequency);
        bool GroundCheck(float offsetX, float offsetZ, out RaycastHit hit)
        {
            return Physics.Raycast(
                new(transform.position.x + offsetX, transform.position.y + 0.05f, transform.position.z + offsetZ),
                Vector3.down, out hit, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);
        }

        if
            (
                grounded &&
                timeSinceLastSafeGroundCheck == safegroundCheckFrequency &&
                GroundCheck(groundedRadius, 0, out var hit1) &&
                GroundCheck(-groundedRadius, 0, out var hit2) &&
                GroundCheck(0, groundedRadius, out var hit3) &&
                GroundCheck(0, -groundedRadius, out var hit4) &&
                Mathf.Approximately(hit1.distance, hit2.distance) &&
                Mathf.Approximately(hit2.distance, hit3.distance) &&
                Mathf.Approximately(hit3.distance, hit4.distance))
        {
            lastSafeGroundedPosition = transform.position;
            timeSinceLastSafeGroundCheck = 0f;
        }
    }

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (input.Look.sqrMagnitude >= Threshold && !lockCameraPosition)
        {
            cinemachineTargetYaw += input.Look.x;
            cinemachineTargetPitch += input.Look.y;
        }

        // clamp our rotations so our values are limited 360 degrees
        cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

        // Cinemachine will follow this target
        cameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + cameraAngleOverride, cinemachineTargetYaw, 0.0f);
    }

    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = input.AlwaysRun ? input.Sprint ? moveSpeed : sprintSpeed : input.Sprint ? sprintSpeed : moveSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (input.Move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(input.Move.x, 0.0f, input.Move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (input.Move != Vector2.zero)
        {
            targetRotation = (Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg) +
                                    mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        // move the player
        controller.Move((targetDirection.normalized * (speed * Time.deltaTime)) +
                            (new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime) + (grounded ? (slidingForce * Time.deltaTime) : Vector3.zero));

        animator.SetFloat("Speed", animationBlend);
        animator.SetFloat("MotionSpeed", 1f);
    }

    private void JumpAndGravity()
    {
        if (grounded)
        {
            // reset the fall timeout timer
            fallTimeoutDelta = fallTimeout;

            animator.SetBool("Jump", false);
            animator.SetBool("FreeFall", false);

            // stop our velocity dropping infinitely when grounded
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }

            // Jump
            if (input.Jump && jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                animator.SetBool("Jump", true);
            }

            // jump timeout
            if (jumpTimeoutDelta >= 0.0f)
            {
                jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            jumpTimeoutDelta = jumpTimeout;

            // fall timeout
            if (fallTimeoutDelta >= 0.0f)
            {
                fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("FreeFall", true);
            }

            // if we are not grounded, do not jump
            input.Jump = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (verticalVelocity < terminalVelocity)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new(1.0f, 0.0f, 0.0f, 0.35f);

        if (grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),
            groundedRadius);
    }
}
