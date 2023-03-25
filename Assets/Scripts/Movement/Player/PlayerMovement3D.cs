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
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);

    [SerializeField] private bool canSprint = true;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] float baseSpeed = 6.0f;
    [SerializeField] private float sprintSpeed = 12.0f;

    private float nextDash = 0.0f;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftAlt;
    [SerializeField] float dashSpeed = 40.0f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 4.0f;

    [SerializeField] float levitationSpeed = 0.01f; // Adjustable levitation speed
    [SerializeField] float maxHeight = 5.0f; // Maximum height to levitate

    [SerializeField] private float currentHeight = 0.0f; // Current levitation height
    private bool isLevitating => canLevitate && Input.GetKey(levitationKey); 
    [SerializeField] private bool canLevitate = true; // Whether or not the player is currently levitating
    [SerializeField] private KeyCode levitationKey = KeyCode.Space;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        //getting input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        //direction Vector
        Vector3 moveDir = new Vector3(horizontalInput, 0f, verticalInput);

        if (moveDir.magnitude > 1.0f)
        {
            moveDir = moveDir.normalized;
        }


        //dash cooldown check
        if (Time.time > nextDash)
        {
            if (Input.GetKey(dashKey))
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