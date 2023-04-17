using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpItems : MonoBehaviour
{
    private GameObject itemToPickUp;
    public EQ_CanvasController controller;
    public List<GameObject> Inventory = new List<GameObject>();

    private void Update() 
    {
        if(itemToPickUp != null && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(itemToPickUp);
            controller.CloseMessagePanel();
            Inventory.Add(itemToPickUp);
            itemToPickUp = null;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Pickable"))
        {
            controller.OpenMessagePanel();
            itemToPickUp = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Pickable"))
        {
            controller.CloseMessagePanel();
            itemToPickUp = null;
        }
    }
}
