using UnityEngine;

public class DisplayInteractPopup : MonoBehaviour
{
    private void Reset()
    {
        this.hideFlags = HideFlags.HideInInspector;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // if (collision.CompareTag("Player"))
        // TODO Display popup
    }

    private void OnTriggerExit(Collider collision)
    {
        // if (collision.CompareTag("Player"))
        // TODO Hide popup
    }
}
