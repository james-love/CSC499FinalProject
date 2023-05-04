using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewShowUI : MonoBehaviour
{
    public GameObject panelOne;
    public GameObject panelTwo;

    void Start()
    {
        panelOne.SetActive(false);
        panelTwo.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("panels set active");
            panelOne.SetActive(true);
            panelTwo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            panelOne.SetActive(false);
            panelTwo.SetActive(false);
        }
    }
}
