using StarterAssets;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator fadeTransition;
    [SerializeField] private GameObject player;
    public static LevelManager Instance { get; private set; }

    public void LoadLevel(int levelIndex, int waypoint)
    {
        StartCoroutine(LoadAsync(levelIndex, waypoint));
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.GetComponent<PlayerInput>().currentActionMap.Disable();
        player.GetComponent<StarterAssetsInputs>().cursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<StarterAssetsInputs>().cursorLocked = true;
        player.GetComponent<PlayerInput>().currentActionMap.Enable();
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator LoadAsync(int levelIndex, int waypoint)
    {
        Pause();
        fadeTransition.SetTrigger("Start");
        yield return new WaitUntil(() => Utility.AnimationFinished(fadeTransition, "TransitionStart"));

        if (levelIndex != SceneManager.GetActiveScene().buildIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
            while (!operation.isDone)
                yield return null;
        }

        foreach (GameObject point in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            if (point.GetComponents<MonoBehaviour>().OfType<ISpawnPoint>().ToArray()?[0]?.SpawnPointIndex == waypoint)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().Teleport(point.transform.position);
                break;
            }
        }

        fadeTransition.SetTrigger("Loaded");
        Resume();
    }
}
