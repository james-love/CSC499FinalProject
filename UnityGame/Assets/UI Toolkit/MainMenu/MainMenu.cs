using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        Button startGame = root.Q<Button>("StartGame");
        startGame.RegisterCallback<ClickEvent>(_ => LevelManager.Instance.LoadLevel(1, 0));
        AddHoverSound(startGame);

        Button quit = root.Q<Button>("ExitGame");
        quit.RegisterCallback<ClickEvent>(_ =>
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        AddHoverSound(quit);
    }

    private void AddHoverSound(Button btn)
    {
        btn.RegisterCallback<MouseOverEvent>(_ => AudioManager.Instance.PlayOneShot(FMODEvents.Instance.OneShotTest, Vector3.zero));
    }
}
