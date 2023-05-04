using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowUI : MonoBehaviour
{
    public GameObject panelOne;
    public GameObject panelTwo;

    bool playerNearby;

    void Start()
    {
        panelOne.SetActive(false);
        panelTwo.SetActive(false);
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
        // if player walks away from npc panels disappear
        if (other.name == "Player")
        {
            playerNearby = false;
            panelOne.SetActive(false);
            panelTwo.SetActive(false);
        }
    }

    void Update()
    {
        // if player is next to npc + interact key pressed
        if (Keyboard.current.fKey.wasPressedThisFrame && playerNearby)
        {
            // turn on the ui panels
            panelOne.SetActive(true);
            panelTwo.SetActive(true);
            Debug.Log("ui activated");


            // not working below

            //if (Keyboard.current.enterKey.wasPressedThisFrame)
            //{
                //panelOne.SetActive(false);
                //panelTwo.SetActive(false);
                //Debug.Log("ui deactivated");
            //}
            
        }
    }

}
