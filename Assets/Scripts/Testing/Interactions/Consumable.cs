using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour, IConsumable
{
    [SerializeField]
    private pickUp itemData;

    public pickUp Consume()
    {
        Destroy(gameObject);
        return itemData;
    }
}
