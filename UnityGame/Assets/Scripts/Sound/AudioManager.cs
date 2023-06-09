using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;

    private EventInstance musicEventInstance;

    public static AudioManager Instance { get; private set; }
    public float MasterVolume { get; set; } = 1;
    public bool MusicMute { get; set; } = false;
    public bool SFXMute { get; set; } = false;

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlayMusic(EventReference sound)
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        InitializeMusic(sound);
    }

    public void PlayOneShotWithParameters(EventReference sound, Vector3 worldPos, string parameterName, float parameterValue)
    {
        EventInstance instance = RuntimeManager.CreateInstance(sound);
        instance.setParameterByName(parameterName, parameterValue);
        instance.set3DAttributes(worldPos.To3DAttributes());
        instance.start();
        instance.release();
    }

    public EventInstance CreateInstance(EventReference eventRef)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventRef);
        return eventInstance;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            masterBus = RuntimeManager.GetBus("bus:/");
            musicBus = RuntimeManager.GetBus("bus:/Music");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");

            if (PlayerPrefs.HasKey("volume"))
                MasterVolume = PlayerPrefs.GetFloat("volume");
            if (PlayerPrefs.HasKey("musicmute"))
                MusicMute = PlayerPrefs.GetInt("musicmute") == 1;
            if (PlayerPrefs.HasKey("sfxmute"))
                SFXMute = PlayerPrefs.GetInt("sfxmute ") == 1;

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeMusic(FMODEvents.Instance.Level01Music);
    }

    private void Update()
    {
        masterBus.setVolume(MasterVolume);
        musicBus.setMute(MusicMute);
        sfxBus.setMute(SFXMute);
    }

    private void InitializeMusic(EventReference musicEvent)
    {
        musicEventInstance = CreateInstance(musicEvent);
        musicEventInstance.start();
    }
}
