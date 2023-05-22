using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;    

    public float getBottomY() {
        Debug.Log(_characterController.height);
        return transform.position.y - _characterController.height/2;
    }
}
