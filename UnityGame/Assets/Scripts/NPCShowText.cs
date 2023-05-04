using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCShowText : MonoBehaviour
{
    bool playerDetected = false; // boolean to activate NPC actions/dialogue
    public GameObject textUI; // the text that will pop up saying 'press a to talk'

    void Start()
    {
        textUI.SetActive(false);
    }

    void Update()
    {
        if (playerDetected && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("NPC dialogue has been activated");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerDetected = true;
            textUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerDetected = false;
        textUI.SetActive(false);
    }
}
