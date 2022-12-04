using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Variables")]
    private float _horizontalMovement;
    private float _verticalMovement;
    public float speed;
    private Camera _mainCamera;

    [Header("References")]
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");
        
        LookAtCursor();

        // Vector3 movementDirection = transform.forward * _verticalMovement + transform.right * _horizontalMovement;
        //
        // _rb.AddForce(movementDirection * speed, ForceMode.Force);
        // _rb.drag = drag;
        //
        // if(movementDirection != Vector3.zero)
        // {
        //     Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        //
        //     transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        // }
        
        
    }

    private void LookAtCursor() {
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
 
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);
 
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void FixedUpdate() {
        Vector3 moveDirection = new Vector3(0,0,0); 
        if (_horizontalMovement != 0) {
            moveDirection += new Vector3(-1,0,1) * -_horizontalMovement;
        }
        if (_verticalMovement != 0) {
            moveDirection += new Vector3(1,0,1) * _verticalMovement;
        }
        MoveToDirection(moveDirection);
    }

    void MoveToDirection(Vector3 direction) {
        if (direction == Vector3.zero) {
            return;
        }
        _rb.AddForce(direction * speed, ForceMode.Force);
    }
}
