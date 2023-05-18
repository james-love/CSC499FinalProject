using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillEnemy : MonoBehaviour
{
    private bool playerNearby;

    // animator to play enemy death
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
            // do enemy death animation here (IF they're a one hit kill)
            Destroy(this.gameObject);

            // if not one hit kill,
            // heavy attack = insta kill enemies but takes longer to swing, would play enemy death animation here
            // light attack = no insta kill but faster, plays enemy recoil/got hit animation here 
        }
    }
}
