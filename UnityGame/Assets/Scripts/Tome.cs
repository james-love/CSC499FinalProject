using StarterAssets;
using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("tome picked up");
            this.gameObject.SetActive(false);
            // or 
            // Destroy(this.gameObject);
        }
    }
}
