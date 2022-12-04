using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Variables")]
    private float horizontalMovement;
    private float verticalMovement;
    public float speed;
    public float drag;
    public float rotationSpeed;

    [Header("References")]
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        rb.AddForce(movementDirection * speed, ForceMode.Force);
        rb.drag = drag;

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
