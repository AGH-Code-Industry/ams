using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    [Header("References")]
    private GameObject _door;

    private void Start()
    {
        _door = this.transform.parent.gameObject;
    }

    private void OnTriggerStay(Collider col)
    {
       if(col.name == "Player" && Input.GetKeyDown(KeyCode.E)) //change name to tag
        {
            _door.SetActive(false);
        }
    }
}
