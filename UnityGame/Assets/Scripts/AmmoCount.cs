using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    // UI object that displays ammo in top left
    public Text ammoCounter;

    [SerializeField] public float remainingAmmo = 5;

    // if shoot, reduce ammo count by one

    private void Start()
    {

    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (remainingAmmo < 1)
            {
                Debug.Log("no more bullets!");
                remainingAmmo = 0;
                ammoCounter.GetComponent<Text>().text = remainingAmmo.ToString();
            }
            else
            {

                Debug.Log("shot fired");
                remainingAmmo -= 1;
                ammoCounter.GetComponent<Text>().text = remainingAmmo.ToString();
            }
        }
    }

    // if button pressed, buy appropriate amount of bullets
}
