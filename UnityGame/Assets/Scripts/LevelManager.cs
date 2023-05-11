using StarterAssets;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator fadeTransition;
    public static LevelManager Instance { get; private set; }

    public void LoadLevel(int levelIndex, int waypoint)
    {
        StartCoroutine(LoadAsync(levelIndex, waypoint));
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
        Time.timeScale = 0;
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
        Time.timeScale = 1;
    }
}
