using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class CollectingCoins : MonoBehaviour
{
    public int coins;

    public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Coin")
        {
            Debug.Log("Coin collected");
            coins = coins + 1;
            Destroy(Col.gameObject);

        }
    }
}