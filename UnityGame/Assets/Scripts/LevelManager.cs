using System.Collections;
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

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        while (!operation.isDone)
            yield return null;

        print(waypoint);

        fadeTransition.SetTrigger("Loaded");
        Time.timeScale = 1;
    }
}
