using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private VisualElement root;
    [SerializeField] private Sprite muteMusicIcon;
    [SerializeField] private Sprite unMuteMusicIcon;

    [SerializeField] private Sprite muteSFXIcon;
    [SerializeField] private Sprite unMuteSFXIcon;
    [SerializeField] private PlayerMovementInputs playerInputs;

    public void Open()
    {
        LevelManager.Instance.Pause();
        UpdateVolumeDisplay();
        root.style.display = DisplayStyle.Flex;
    }

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        root.style.display = DisplayStyle.None;

        root.Q<Button>("Resume").RegisterCallback<ClickEvent>(_ =>
        {
            root.style.display = DisplayStyle.None;
            LevelManager.Instance.Resume();
        });

        root.Q<Button>("MuteMusic").RegisterCallback<ClickEvent>(_ =>
        {
            AudioManager.Instance.MusicMute = !AudioManager.Instance.MusicMute;
            PlayerPrefs.SetInt("musicmute", AudioManager.Instance.MusicMute ? 1 : 0);
            UpdateVolumeDisplay();
        });

        root.Q<Button>("MuteSFX").RegisterCallback<ClickEvent>(_ =>
        {
            AudioManager.Instance.SFXMute = !AudioManager.Instance.SFXMute;
            PlayerPrefs.SetInt("sfxmute", AudioManager.Instance.SFXMute ? 1 : 0);
            UpdateVolumeDisplay();
        });

        root.Q<Slider>("VolumeLevel").RegisterValueChangedCallback(e =>
        {
            AudioManager.Instance.MasterVolume = e.newValue;
            PlayerPrefs.SetFloat("volume", e.newValue);
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

    private void Start()
    {
        Toggle alwaysRun = root.Q<Toggle>("AlwaysRun");
        alwaysRun.RegisterValueChangedCallback(e => playerInputs.SetAlwaysRun(e.newValue));
        alwaysRun.value = playerInputs.AlwaysRun;
    }

    private void UpdateVolumeDisplay()
    {
        root.Q<Button>("MuteMusic").style.backgroundImage = new StyleBackground(AudioManager.Instance.MusicMute ? muteMusicIcon : unMuteMusicIcon);
        root.Q<Button>("MuteSFX").style.backgroundImage = new StyleBackground(AudioManager.Instance.SFXMute ? muteSFXIcon : unMuteSFXIcon);
        root.Q<Slider>("VolumeLevel").value = AudioManager.Instance.MasterVolume;
    }
}
