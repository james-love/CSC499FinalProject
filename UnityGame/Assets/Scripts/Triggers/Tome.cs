using UnityEngine;

public class Tome : MonoBehaviour
{
    // Object will be destroyed, this is just here to prevent a possible race condition
    private bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            collected = true;
            HUDManager.Instance.TomeCollected();
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.TomeCollected, transform.position);
            Destroy(gameObject); // TODO: Replace with animation
        }
    }
}
