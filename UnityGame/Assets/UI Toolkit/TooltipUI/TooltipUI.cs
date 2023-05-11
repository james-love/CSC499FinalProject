using UnityEngine;
using UnityEngine.UIElements;

public class TooltipUI : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string tooltipText;
    [SerializeField] private float fontSize;
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;

        Label tooltip = root.Q<Label>("TooltipText");
        tooltip.text = tooltipText;
        tooltip.style.fontSize = fontSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            root.style.display = DisplayStyle.Flex;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            root.style.display = DisplayStyle.None;
    }
}
