using UnityEngine;
using UnityEngine.UIElements;

public class DialoguePopup : MonoBehaviour
{
    private VisualElement root;

    public void SetValues(string text, int width, int height, int fontSize)
    {
        VisualElement popup = root.Q<VisualElement>("Popup");
        popup.style.width = width;
        popup.style.height = height;

        Label label = root.Q<Label>("Text");
        label.style.fontSize = fontSize;
        label.text = text;
    }

    public void Display()
    {
        Display(root.style.display == DisplayStyle.None);
    }

    public void Display(bool newValue)
    {
        root.style.display = newValue ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
    }
}
