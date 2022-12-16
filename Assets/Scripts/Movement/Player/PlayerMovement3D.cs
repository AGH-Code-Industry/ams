using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private CharacterController characterController;
    
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    
    [SerializeField] private bool canSprint = true;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] float baseSpeed = 6.0f;
    [SerializeField] private float sprintSpeed = 12.0f;

    private float nextDash = 0.0f;
    [SerializeField] private KeyCode dashKey = KeyCode.Space;
    [SerializeField] float dashSpeed = 40.0f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 4.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //getting input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        //moving 
        Vector3 moveDir = new Vector3(horizontalInput, 0f,verticalInput);
        
        if (moveDir.magnitude > 1.0f)
        {
            moveDir = moveDir.normalized;
        }

        if (Time.time > nextDash)
        {
            if (Input.GetKey(dashKey))
            {
                StartCoroutine(Dash(moveDir));
                nextDash = Time.time + dashCooldown;
            }
        }
        
        //transform.Translate(moveDir * (isSprinting ? sprintSpeed : baseSpeed) * Time.deltaTime);
        characterController.SimpleMove(transform.TransformVector(moveDir)  * (isSprinting ? sprintSpeed : baseSpeed));

    }
    
    IEnumerator Dash(Vector3 dir)
    {
        float startTime= Time.time;

        while (Time.time < startTime + dashTime)
        {
            // TODO:
            transform.Translate(dir * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }
    
}
