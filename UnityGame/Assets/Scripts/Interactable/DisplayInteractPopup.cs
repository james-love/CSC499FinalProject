using UnityEngine;

public class DisplayInteractPopup : MonoBehaviour
{
    private GameObject popup;

    private void Awake()
    {
        popup = Instantiate(Resources.Load<GameObject>("InteractPopup"));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
            popup.SetActive(true);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
            popup.SetActive(false);
    }
}
