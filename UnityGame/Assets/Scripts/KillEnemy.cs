using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillEnemy : MonoBehaviour
{
    public bool playerNearby;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerNearby = false;
        }
    }

    private void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame && playerNearby == true)
        {
            Destroy(this.gameObject);
        }
    }
}
