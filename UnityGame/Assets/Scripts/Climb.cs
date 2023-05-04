using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Climb : MonoBehaviour
{
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;
    public ThirdPersonController playerMovement;
    public InputAction inputActions;
    private Animator animator;

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;
    private bool isClimbing;

    public float detectLength;
    public float castRadius;
    public float maxWallLookAnngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private void Start()
    {
        animator = playerMovement.GetComponent<Animator>();
        //rb = playerMovement.GetComponent<Rigidbody>();
        // don't really need since messing with rb didn't work anyways
    }
    private void Update()
    {
        
        WallCheck();
        StateMachine();

        if (isClimbing)
        {
            ClimbMovement();
        }
    }

    // to determine when to start/stop climbing
    private void StateMachine()
    {
        var keyboard = Keyboard.current;

        if (wallFront && keyboard.cKey.wasPressedThisFrame && wallLookAngle < maxWallLookAnngle)
        {
            Debug.Log("climb");
            animator.Play("mixamo_com");

            if (!isClimbing && climbTimer > 0)
            {
                StartClimb();
                
            }

            if (climbTimer > 0)
            {
                climbTimer -= Time.deltaTime;
            }

            if (climbTimer < 0)
            {
                StopClimb();
            }

        }
    }
    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, castRadius, orientation.forward, out frontWallHit, detectLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        // reset climb timer
        if (playerMovement.Grounded)
        {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimb()
    {
        isClimbing = true;
        Debug.Log("start climb");
    }

    private void StopClimb()
    {
        isClimbing = false;
        Debug.Log("stop climb");

        Vector3 direction = Vector3.zero;
        direction.y = 0;
        playerMovement._verticalVelocity = 0;
    }

    private void ClimbMovement()
    {
        // below line not currently working
        //rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);

        Debug.Log("climb movement");
        if (playerMovement.Grounded == true)
        {
            Vector3 direction = Vector3.up;
            direction.Normalize();
            direction.x = 0;
            direction.z = 0;
            playerMovement._verticalVelocity = 0.2f;
        }
        playerMovement._verticalVelocity -= playerMovement.Gravity;
        playerMovement.Move();
        isClimbing = true;

        StopClimb();
    }

}
