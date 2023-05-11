using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewShowUI : MonoBehaviour
{
    public GameObject text;

    void Start()
    {
        text.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("panels set active");
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            text.SetActive(false);
        }
    }
}
