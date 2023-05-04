using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowUI : MonoBehaviour
{
    public GameObject panelOne;
    public GameObject panelTwo;

    void Start()
    {
        panelOne.SetActive(false);
        panelTwo.SetActive(false);
    }

    
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            panelOne.SetActive(true);
            panelTwo.SetActive(true);
            Debug.Log("ui activated");


            // not working
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                panelOne.SetActive(false);
                panelTwo.SetActive(false);
                Debug.Log("ui deactivated");
            }
            //StartCoroutine(Wait());
        }

        //IEnumerator Wait()
        //{
            //yield return new WaitForSeconds(5);
           // panelOne.SetActive(false);
           // panelTwo.SetActive(false);
           // isUIOn = false;
           // Debug.Log("ui deactivated");
        //}
    }
}
