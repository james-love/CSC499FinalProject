using UnityEngine;

public class LevelExit : MonoBehaviour, ISpawnPoint
{
    [SerializeField] private int goToScene;
    [SerializeField] private int goToSpawnPoint;
    [field: SerializeField] public int SpawnPointIndex { get; set; }
    public bool HasRotation { get => false; set => throw new System.NotImplementedException(); }
    public float Rotation { get => 0f; set => throw new System.NotImplementedException(); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.LoadLevel(goToScene, goToSpawnPoint);
        }
    }
}
