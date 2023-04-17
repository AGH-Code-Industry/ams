using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = InputManager.actions.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(moveVector.x, 0f, moveVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction.normalized * speed * Time.deltaTime);
        }
    }
}
