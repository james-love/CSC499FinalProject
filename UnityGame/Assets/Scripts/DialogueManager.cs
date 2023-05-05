using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public GameObject Dialogue; // what the npc will actually say
    public GameObject interactInstructions; // the "press _ to interact" that will appear
    private bool inRange;

    private void Start()
    {
        // have everything initially false
        inRange = false;
        interactInstructions.SetActive(false);
        Dialogue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
            interactInstructions.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            interactInstructions.SetActive(false);
            Dialogue.SetActive(false);
        }
    }

    void ShowDialogue()
    {
        Dialogue.SetActive(true);
    }

    private void Update()
    {
        if (inRange && Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("f key pressed");
            ShowDialogue();
        }
    }
}
