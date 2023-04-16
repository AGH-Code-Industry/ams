using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private CharacterController characterController;

    [SerializeField] private LayerMask aimLayerMask;
    private bool isSprinting => canSprint && InputManager.actions.Player.Sprint.IsPressed();

    [SerializeField] private bool canSprint = true;
    [SerializeField] float baseSpeed = 6.0f;
    [SerializeField] private float sprintSpeed = 12.0f;

    private float nextDash = 0.0f;
    [SerializeField] float dashSpeed = 40.0f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 4.0f;

    [SerializeField] float levitationSpeed = 0.01f; // Adjustable levitation speed
    [SerializeField] float maxHeight = 5.0f; // Maximum height to levitate

    [SerializeField] private float currentHeight = 0.0f; // Current levitation height
    private bool isLevitating => canLevitate && InputManager.actions.Player.Levitate.IsPressed(); 
    [SerializeField] private bool canLevitate = true; // Whether or not the player is currently levitating

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //getting input
        Vector2 moveVector = InputManager.actions.Player.Move.ReadValue<Vector2>();
        horizontalInput = moveVector.x;
        verticalInput = moveVector.y;


        //direction Vector
        Vector3 moveDir = new Vector3(horizontalInput, 0f, verticalInput);

        if (moveDir.magnitude > 1.0f)
        {
            moveDir = moveDir.normalized;
        }


        //dash cooldown check
        if (Time.time > nextDash)
        {
            if (InputManager.actions.Player.Dash.IsPressed())
            {
                StartCoroutine(Dash(moveDir));
                nextDash = Time.time + dashCooldown;
            }
        }

       

        if (isLevitating)
        {
            currentHeight += levitationSpeed * Time.deltaTime;
            if (currentHeight > maxHeight)
            {
                currentHeight = maxHeight;
            }
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        }
        else
        {
            //moving
            characterController.SimpleMove(moveDir * (isSprinting ? sprintSpeed : baseSpeed));
            currentHeight = transform.position.y;


        }
    }

    IEnumerator Dash(Vector3 dir)
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            characterController.SimpleMove(transform.TransformVector(dir * dashSpeed));
            yield return null;
        }
    }

}