using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float invulnerableAfterDeathTime = 2f;
    [SerializeField] private Animator playerAnimator;
    private int currentHealth;
    private VisualElement root;
    private bool invulnerable = false;
    public static HealthManager Instance { get; private set; }
    public int DeathCount { get; private set; } = 0;

    public void AdjustHealth(int adjustment)
    {
        if (!invulnerable)
        {
            // TODO Player hit graphics and sound here
            currentHealth = Mathf.Clamp(currentHealth + adjustment, 0, maxHealth);
            HUDManager.Instance.HealthDisplay(currentHealth);
            if (currentHealth == 0)
            {
                invulnerable = true;
                StartCoroutine(PlayerDeath());
            }
            else
            {
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.PlayerHit, playerAnimator.transform.position);
            }
        }
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        HUDManager.Instance.HealthDisplay(currentHealth);
        playerAnimator.SetTrigger("Respawn");
        HUDManager.Instance.Resume();
        StartCoroutine(InvulnerableTime());
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            currentHealth = maxHealth;
            root = GetComponent<UIDocument>().rootVisualElement;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        HUDManager.Instance.HealthDisplay(currentHealth, maxHealth);
    }

    private IEnumerator PlayerDeath()
    {
        DeathCount += 1;
        HUDManager.Instance.Pause(false);
        playerAnimator.SetTrigger("Die");
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.PlayerDeath, playerAnimator.transform.position);
        yield return new WaitUntil(() => Utility.AnimationFinished(playerAnimator, "Dead", 3));
        Time.timeScale = 0;
        root.style.display = DisplayStyle.Flex;
    }

    private IEnumerator InvulnerableTime()
    {
        yield return new WaitForSeconds(invulnerableAfterDeathTime);
        invulnerable = false;
    }
}
