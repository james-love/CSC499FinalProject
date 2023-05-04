using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tome : MonoBehaviour
{
    public int tomeCounter;
    //public ThirdPersonController playerController;

    private void Start()
    {
        this.gameObject.SetActive(true);
        //playerController = GetComponent<ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("tome picked up");
            this.gameObject.SetActive(false);
            // or 
            // Destroy(this.gameObject);
            tomeCounter++;

            //playerController.tomeCounter++;
        }
    }
}
