using UnityEngine;

public class Tome : MonoBehaviour
{
    // don't forget to tag the tomes as "Tome"
    private void Start()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
