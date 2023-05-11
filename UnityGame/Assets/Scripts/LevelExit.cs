using UnityEngine;

public class LevelExit : Interactable, ISpawnPoint
{
    [SerializeField] private int goToScene;
    [SerializeField] private int goToSpawnPoint;
    [field: SerializeField] public int SpawnPointIndex { get; set; }

    public override void Interact()
    {
        LevelManager.Instance.LoadLevel(goToScene, goToSpawnPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print(goToSpawnPoint);
            LevelManager.Instance.LoadLevel(goToScene, goToSpawnPoint);
        }
    }
}
