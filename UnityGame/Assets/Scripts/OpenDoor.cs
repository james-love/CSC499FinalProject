using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    public bool oneKey;
    public bool threeKey;
    public bool fiveKey;
    public bool sevenKey;
    public bool zeroKey;

    public GameObject door;
    public GameObject puzzleUI;
    public bool inRange;

    public string currentCombination = "";
    public string correctCombination = "13570"; // Set the correct combination here

    private void Update()
    {
        if (inRange == true)
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                currentCombination += "1";
            }
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                currentCombination += "2";
            }
            if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                currentCombination += "3";
            }
            if (Keyboard.current.digit4Key.wasPressedThisFrame)
            {
                currentCombination += "4";
            }
            if (Keyboard.current.digit5Key.wasPressedThisFrame)
            {
                currentCombination += "5";
            }
            if (Keyboard.current.digit6Key.wasPressedThisFrame)
            {
                currentCombination += "6";
            }
            if (Keyboard.current.digit7Key.wasPressedThisFrame)
            {
                currentCombination += "7";
            }
            if (Keyboard.current.digit8Key.wasPressedThisFrame)
            {
                currentCombination += "8";
            }
            if (Keyboard.current.digit9Key.wasPressedThisFrame)
            {
                currentCombination += "9";
            }
            if (Keyboard.current.digit0Key.wasPressedThisFrame)
            {
                currentCombination += "0";
            }

            if (CheckCombination())
            {
                Debug.Log("Door opened, correct keys pressed");
                door.SetActive(false);
                puzzleUI.SetActive(false);
            }
        }
    }

    private bool CheckCombination()
    {
        if (currentCombination.Length == correctCombination.Length)
        {
            if (currentCombination == correctCombination)
            {
                return true;
            }
            else
            {
                Debug.Log("Incorrect combination of keys. Resetting entry...");
                currentCombination = "";
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            puzzleUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            puzzleUI.SetActive(false);
            oneKey = false;
            threeKey = false;
            fiveKey = false;
            sevenKey = false;
            zeroKey = false;
        }
    }   

}
