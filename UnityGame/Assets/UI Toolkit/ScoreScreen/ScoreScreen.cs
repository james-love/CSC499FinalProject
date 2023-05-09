using HighScore;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreScreen : MonoBehaviour
{
    private readonly string placeHolderText = "Input name...";
    private VisualElement root;

    private void Awake()
    {
        Time.timeScale = 0;
        HS.Init(this, "TempName01");

        root = GetComponent<UIDocument>().rootVisualElement;

        TextField name = root.Q<TextField>("NameInput");
        name.SetPlaceholderText(placeHolderText);

        Button submitButton = root.Q<Button>("SubmitButton");
        submitButton.clicked += SubmitScore;
    }

    private void SubmitScore()
    {
        TextField name = root.Q<TextField>("NameInput");
        if (!string.IsNullOrWhiteSpace(name.value) && name.value != placeHolderText && int.TryParse(root.Q<Label>("FinalValue").text, out int score))
            HS.SubmitHighScore(this, name.value, score);
    }
}
