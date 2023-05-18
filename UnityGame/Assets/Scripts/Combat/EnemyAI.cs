using UnityEngine;

public enum EnemyState
{
    Idle,
    Seeking,
    Attack,
    Dead,
}

public class EnemyAI : Enemy
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float gravity = 2.0f;
    [SerializeField] private int totalHealth = 2;
    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float attackRange = 1.5f;
    private int currentHealth;
    [SerializeField] private EnemyState currentState = EnemyState.Idle;
    private CharacterController controller;
    private Animator animator;
    private Transform playerTransform;

    private bool playerSpotted = false;
    private bool attacking = false;
    private bool playerHit = false;

    public override void Hit(int damageValue)
    {
        // TODO enemy hit graphics and sound here
        currentHealth = Mathf.Clamp(currentHealth - damageValue, 0, totalHealth);
        if (currentHealth == 0)
        {
            animator.SetTrigger("Die");
            controller.enabled = false;
            currentState = EnemyState.Dead;
        }
    }

    public void HandTriggerEnter(Collider other)
    {
        if (!playerHit && attacking && other.CompareTag("Player"))
        {
            playerHit = true;
            HealthManager.Instance.AdjustHealth(-1);
        }
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = totalHealth;
    }

    private void Update()
    {
        playerSpotted = Vector3.Distance(transform.position, playerTransform.position) < sightRange;
        animator.SetBool("PlayerSpotted", playerSpotted);
        switch (currentState)
        {
            case EnemyState.Idle:
                if (playerSpotted)
                    currentState = EnemyState.Seeking;
                break;
            case EnemyState.Seeking:
                if (!playerSpotted)
                {
                    currentState = EnemyState.Idle;
                }
                else if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
                {
                    attacking = true;
                    playerHit = false;
                    currentState = EnemyState.Attack;
                    animator.SetTrigger("Attack");
                }
                else
                {
                    Vector3 velocity = Vector3.zero;
                    if (controller.isGrounded == true)
                    {
                        Vector3 direction = playerTransform.position - transform.position;
                        direction.Normalize();
                        direction.y = 0;
                        transform.localRotation = Quaternion.LookRotation(direction);
                        velocity = direction * speed;
                    }

                    velocity.y -= gravity;
                    controller.Move(velocity * Time.deltaTime);
                }

                break;
            case EnemyState.Attack:
                if (attacking)
                {
                    if (Utility.AnimationFinished(animator, "EnemyAttack"))
                    {
                        animator.SetTrigger("AttackFinished");
                        attacking = false;
                    }
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyWalk"))
                {
                    currentState = EnemyState.Seeking;
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle"))
                {
                    currentState = EnemyState.Idle;
                }

                break;
            case EnemyState.Dead:
                break;
            default:
                break;
        }
    }
}
