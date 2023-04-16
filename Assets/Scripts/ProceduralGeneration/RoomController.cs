using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("References")]
    private List<GameObject> _doors;
    private bool _isOpened;
    private float _doorCooldown;

    private void Awake()
    {
        _doors = new List<GameObject>();

        foreach(Transform child in gameObject.transform)
        {         
            if(child.CompareTag("Door"))
            {
                _doors.Add(child.gameObject);
            }          
        }

        _doorCooldown = 2f;
    }

    private void Start()
    {
        OpenAllDoors();
        InputManager.actions.Player.Interact.started += _ => TryInverseDoorState();
    }

    private void TryInverseDoorState()
    {
        if (_doorCooldown <= 0)
        {
            InverseDoorState();
            _doorCooldown = 2f;
        }
    }

    private void Update()
    {
        _doorCooldown -= Time.deltaTime;
    }

    public void OpenAllDoors()
    {
        foreach(GameObject door in _doors)
        {
            door.SetActive(false);
            _isOpened = true;
        }
    }

    public void CloseAllDoors()
    {
        foreach (GameObject door in _doors)
        {
            door.SetActive(true);
            _isOpened = false;
        }
    }

    public void InverseDoorState()
    {
        if(_isOpened)
        {
            CloseAllDoors();
        }
        else
        {
            OpenAllDoors();
        }
    }
}

