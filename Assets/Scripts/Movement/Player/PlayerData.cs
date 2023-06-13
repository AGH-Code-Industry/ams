using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;    

    public float getBottomY() {        
        return transform.position.y;
    }
}
