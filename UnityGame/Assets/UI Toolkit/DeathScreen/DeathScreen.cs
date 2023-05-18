using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathScreen : MonoBehaviour
{
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;

        root.Q<Button>("Continue").RegisterCallback<ClickEvent>(_ =>
        {
            root.style.display = DisplayStyle.None;
            HealthManager.Instance.Respawn();
        });

        root.Q<Button>("Quit").RegisterCallback<ClickEvent>(_ =>
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
