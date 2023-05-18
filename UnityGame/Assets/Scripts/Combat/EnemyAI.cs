using System.Collections;
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
    [SerializeField] private Material flashMaterial;
    [SerializeField] private ScoreScreen scoreScreen; // Only need to assign for the boss
    private CharacterController controller;
    private Animator animator;
    private Transform playerTransform;
    private SkinnedMeshRenderer enemyMesh;

    private bool playerSpotted = false;
    private bool attacking = false;
    private bool playerHit = false;
    private bool invulnerable = false;

    public override void Hit(int damageValue)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            StartCoroutine(InvulnerableTime());
            // TODO enemy hit graphics and sound here
            currentHealth = Mathf.Clamp(currentHealth - damageValue, 0, totalHealth);
            StartCoroutine(EnemyFlash());
            if (currentHealth == 0)
            {
                if (gameObject.name != "TheBoss")
                {
                    animator.SetTrigger("Die");
                    controller.enabled = false;
                    currentState = EnemyState.Dead;
                }
                else
                {
                    StartCoroutine(GameEnd());
                }
            }
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

    private IEnumerator GameEnd()
    {
        animator.SetTrigger("Die");
        controller.enabled = false;
        currentState = EnemyState.Dead;
        yield return new WaitUntil(() => Utility.AnimationFinished(animator, "EnemyDeath"));
        scoreScreen.Open();
    }

    private IEnumerator EnemyFlash()
    {
        var oldMaterial = enemyMesh.material;
        enemyMesh.material = flashMaterial;
        yield return new WaitForSeconds(0.3f);
        enemyMesh.material = oldMaterial;
    }

    private IEnumerator InvulnerableTime()
    {
        yield return new WaitForSeconds(1.5f);
        invulnerable = false;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = totalHealth;
        enemyMesh = GetComponentInChildren<SkinnedMeshRenderer>();
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
