using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMoreBullets : MonoBehaviour
{
    public AmmoCount ammoCount;
    public void BuyBullets()
    {
        Debug.Log("buy bullets");
        ammoCount.remainingAmmo += 5;
    }
}
