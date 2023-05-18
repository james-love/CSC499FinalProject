using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }
    [field: SerializeField] public EventReference TitleThemeMusic { get; private set; }
    [field: SerializeField] public EventReference Level01Music { get; private set; }
    [field: SerializeField] public EventReference Level02Music { get; private set; }
    [field: SerializeField] public EventReference Level03Music { get; private set; }
    [field: SerializeField] public EventReference EndThemMusic { get; private set; }
    [field: SerializeField] public EventReference PlayerFootstep { get; private set; }
    [field: SerializeField] public EventReference PlayerDeath { get; private set; }
    [field: SerializeField] public EventReference PlayerHit { get; private set; }
    [field: SerializeField] public EventReference PlayerJump { get; private set; }
    [field: SerializeField] public EventReference EnemyDeath { get; private set; }
    [field: SerializeField] public EventReference EnemyAttack { get; private set; }
    [field: SerializeField] public EventReference EnemyHit { get; private set; }
    [field: SerializeField] public EventReference UIOpen { get; private set; }
    [field: SerializeField] public EventReference UIClose { get; private set; }
    [field: SerializeField] public EventReference UIHover { get; private set; }
    [field: SerializeField] public EventReference TomeCollected { get; private set; }
    [field: SerializeField] public EventReference TomeNearby { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
