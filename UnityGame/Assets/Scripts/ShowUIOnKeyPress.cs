using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowUIOnKeyPress : MonoBehaviour
{
    public GameObject ui; // the actual dialogue that will appear
    public GameObject interactInstructions; // "press _ to interact"

    private bool playerNearby;

    void Start()
    {
        // default them to false
        ui.SetActive(false);
        interactInstructions.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerNearby = true;
            Debug.Log("interact intstructions activated");
            interactInstructions.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if player walks away from npc panels disappear
        if (other.name == "Player")
        {
            playerNearby = false;
            ui.SetActive(false);

        }
    }

    void Update()
    {
        // if player is next to npc + interact key pressed
        if (Keyboard.current.fKey.wasPressedThisFrame && playerNearby == true)
        {
            interactInstructions.SetActive(false);
            ui.SetActive(true);
            Debug.Log("dialogue activated"); 
        }
    }

}
