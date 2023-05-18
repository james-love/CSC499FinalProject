using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject playerActions;
    [SerializeField] private Animator doorAnimator;
    private bool timerPaused = true;
    private Label timerText;
    private VisualElement root;
    public static HUDManager Instance { get; private set; }
    public int TomesCollected { get; private set; } = 0;
    public float Timer { get; private set; } = 0f;

    public void Pause(bool withTimescale = true)
    {
        if (withTimescale)
            Time.timeScale = 0f;
        timerPaused = true;
        playerActions.GetComponent<PlayerInput>().currentActionMap.Disable();
        playerActions.GetComponent<PlayerMovementInputs>().CursorLocked = false;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        playerActions.GetComponent<PlayerMovementInputs>().CursorLocked = true;
        playerActions.GetComponent<PlayerInput>().currentActionMap.Enable();
        timerPaused = false;
        Time.timeScale = 1f;
    }

    public void TomeCollected()
    {
        TomesCollected += 1;
        root.Q<Label>("TomeCounter").text = $"Tomes: {TomesCollected}";
        if (TomesCollected == 1) // TODO set to 4
            StartCoroutine(OpenDoor());
    }

    public void StartTimer()
    {
        timerPaused = false;
    }

    public void HealthDisplay(int currentHealth, int maxHealth)
    {
        root.Q<ProgressBar>("Health").highValue = maxHealth;
        HealthDisplay(currentHealth);
    }

    public void HealthDisplay(int currentHealth)
    {
        root.Q<ProgressBar>("Health").value = currentHealth;
    }

    private IEnumerator OpenDoor()
    {
        Pause(false);
        doorAnimator.SetTrigger("Open");
        yield return new WaitUntil(() => Utility.AnimationFinished(doorAnimator, "Door Opens"));
        Resume();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            root = GetComponent<UIDocument>().rootVisualElement;

            timerText = root.Q<Label>("Timer");

            StartTimer(); // TODO Move this to after the cutscene ends
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!timerPaused)
            Timer += Time.deltaTime;

        timerText.text = $"{Timer / 60:00}:{Timer % 60:00}";
    }
}
