using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : Interactable, ISpawnPoint
{
    [field: SerializeField] public int SpawnPointIndex { get; set; }

    public override void Interact()
    {
        print("save game here");
        //Player.Instance.State.SaveGame(SceneManager.GetActiveScene().buildIndex, SpawnPointIndex);
    }
}
