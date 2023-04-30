using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpItems : MonoBehaviour
{
    private GameObject itemToPickUp;
    public EQ_CanvasController controller;
    public List<GameObject> Inventory = new List<GameObject>();


    // Made a small change to support the new Input system (Konrad)
    private void Awake()
    {
        InputManager.actions.Player.Interact.started += _ => PickUpAction();
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

    // The new method that is called whenever the Interaction button is pressed
    private void PickUpAction()
    {
        if (itemToPickUp != null)
        {
            Destroy(itemToPickUp);
            controller.CloseMessagePanel();
            Inventory.Add(itemToPickUp);
            itemToPickUp = null;
        }
    }
}
