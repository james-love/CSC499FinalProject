using HighScore;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreScreen : MonoBehaviour
{
    private readonly string placeHolderText = "Input name...";
    private VisualElement root;
    private int totalTomes = 0;

    public void Open()
    {
        HUDManager.Instance.Pause();
        UpdateDisplay();
        root.style.display = DisplayStyle.Flex;
    }

    private void Awake()
    {
        HS.Init(this, "Ascent into Madness");

        root = GetComponent<UIDocument>().rootVisualElement;

        root.style.display = DisplayStyle.None;

        TextField name = root.Q<TextField>("NameInput");
        name.SetPlaceholderText(placeHolderText);

        Button submitButton = root.Q<Button>("SubmitButton");
        submitButton.clicked += SubmitScore;

        totalTomes = GameObject.FindGameObjectsWithTag("Tome").Length;
    }

    private void UpdateDisplay()
    {
        float elapsedTime = HUDManager.Instance.Timer;
        int deaths = HealthManager.Instance.DeathCount;
        int tomes = HUDManager.Instance.TomesCollected;

        root.Q<Label>("TimeValue").text = $"{elapsedTime / 60:00}:{elapsedTime % 60:00}";
        root.Q<Label>("TomesValue").text = $"{tomes}/{totalTomes}";
        root.Q<Label>("DeathsValue").text = deaths.ToString();
        root.Q<Label>("FinalValue").text = Mathf.Clamp(20_000 - (deaths * 100) - (Mathf.FloorToInt(elapsedTime) * 10) - ((totalTomes - tomes) * 25), 0, 20_000).ToString();
    }

    private void SubmitScore()
    {
        TextField name = root.Q<TextField>("NameInput");
        if (!string.IsNullOrWhiteSpace(name.value) && name.value != placeHolderText && int.TryParse(root.Q<Label>("FinalValue").text, out int score))
        {
            HS.SubmitHighScore(this, name.value, score);
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
