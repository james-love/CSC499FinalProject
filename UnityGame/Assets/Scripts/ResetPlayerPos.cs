using StarterAssets;
using UnityEngine;

public class ResetPlayerPos : MonoBehaviour
{
    private ThirdPersonController player;

    private void Start()
    {
        player = GetComponent<ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player position: " + player.lastGroundedPos + "and player velocity: " + player.lastGroundedVelocity);
            player.ResetPos();
        }
    }
}
