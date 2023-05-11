using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] public TextMeshProUGUI tomesText;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("uimanager is null");
            }
            return instance;
        }
        
    }

    private void Awake()
    {
        instance = this;
    }

    public void updateCounter(int tomesCollected)
    {
        // make sure tomes are TAGGED !
        tomesText.text = "Tomes: " + tomesCollected;
    }
}
