using HighScore;
using StarterAssets;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreScreen : MonoBehaviour
{
    private readonly string placeHolderText = "Input name...";
    private VisualElement root;
    private float startTime;

    public void Open()
    {
        LevelManager.Instance.Pause();
        root.style.display = DisplayStyle.Flex;
    }

    private void Awake()
    {
        HS.Init(this, "Ascent into Madness");
        startTime = Time.time;

        root = GetComponent<UIDocument>().rootVisualElement;

        root.style.display = DisplayStyle.None;

        TextField name = root.Q<TextField>("NameInput");
        name.SetPlaceholderText(placeHolderText);

        Button submitButton = root.Q<Button>("SubmitButton");
        submitButton.clicked += SubmitScore;
    }

    private void UpdateDisplay()
    {
        int elapsedTime = Mathf.FloorToInt(Time.time - startTime);
        int minutes = elapsedTime / 60;
        int seconds = elapsedTime % 60;

        int deaths = 0;
        int tomes = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().tomesCollected;
        int maxTomes = 8;

        root.Q<Label>("TimeValue").text = $"{minutes}:{seconds}";
        root.Q<Label>("TomesValue").text = $"{tomes}/{maxTomes}";
        root.Q<Label>("DeathsValue").text = deaths.ToString();
        root.Q<Label>("FinalValue").text = Mathf.Clamp(20_000 - (deaths * 100) - (elapsedTime * 10) - ((maxTomes - tomes) * 25), 0, 20_000).ToString();
    }

    private void SubmitScore()
    {
        TextField name = root.Q<TextField>("NameInput");
        if (!string.IsNullOrWhiteSpace(name.value) && name.value != placeHolderText && int.TryParse(root.Q<Label>("FinalValue").text, out int score))
            HS.SubmitHighScore(this, name.value, score);
    }
}
