using UnityEngine;

public class TheBoss : MonoBehaviour
{
    [SerializeField] private ScoreScreen scoreScreen;

    private void OnDestroy()
    {
        scoreScreen.Open();
    }
}
