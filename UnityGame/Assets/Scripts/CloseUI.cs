using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : MonoBehaviour
{
    public GameObject panelOne;
    public GameObject panelTwo;

    public void Close()
    {
        panelOne.SetActive(false);
        panelTwo.SetActive(false);
    }
}
