using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Camera mainCam;
    public Transform target;
    private float horizontalInput;
    private float verticalInput;
    private bool sprintInput;
    private bool attackInput;
    private bool dashInput;
    private Animator anim;

    public float temporaryMultiplier;

    public Vector2 mousePos;
    public Vector2 screenPos;
    public Vector3 moveDir;
    public Vector3 mouseOffset;
    public float velocityY = 0;
    public float velocityX = 0;


    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        sprintInput = Input.GetKey(KeyCode.LeftShift);
        attackInput = Input.GetKey(KeyCode.Mouse0);
        dashInput = Input.GetKey(KeyCode.Space);
        
        moveDir = new Vector3(horizontalInput, 0f,verticalInput);  
        mousePos = Input.mousePosition;
        screenPos = mainCam.WorldToScreenPoint(target.position);
        mouseOffset = (mousePos - screenPos).normalized;
        mouseOffset = new Vector3(mouseOffset.x, 0, mouseOffset.y);
        float angle = Vector3.SignedAngle(moveDir, mouseOffset, Vector3.up);

        
        Debug.Log(angle);
        Debug.Log("VelocityX" + velocityX);
        Debug.Log("VelocityY" + velocityY);
        velocityX = -Mathf.Sin(angle * Mathf.Deg2Rad) * moveDir.magnitude / temporaryMultiplier;    
        velocityY = Mathf.Cos(angle * Mathf.Deg2Rad) * moveDir.magnitude / temporaryMultiplier;
        if (sprintInput) {
            velocityX *= temporaryMultiplier;
            velocityY *= temporaryMultiplier;
        }

        Debug.DrawRay(transform.position, new Vector3(velocityX, 0 , velocityY));
        //animating
        
        // if (mouseOffset.x > 0)
        // {
        //     if (mouseOffset.y > 0)
        //     {
        //         velocityY = (moveDir.x * mouseOffset.x) + mouseOffset.y;
        //         velocityX = -(moveDir.z * mouseOffset.y) + mouseOffset.x;
        //     }
        //     else
        //     {
        //         velocityY = (moveDir.z + mouseOffset.x);
        //         velocityX = -(moveDir.x + mouseOffset.y);
        //     }
        //     
        // } 
        // else if (mouseOffset.y > 0)
        // {
        //     velocityY = (moveDir.z + mouseOffset.x);
        //     velocityX = -(moveDir.x + mouseOffset.y);
        // }
        // else
        // {
        //     velocityY = (moveDir.z + mouseOffset.x);
        //     velocityX = -(moveDir.x + mouseOffset.y);
        // }
        
        
        
        anim.SetFloat("VelocityZ", velocityY, 0.1f, Time.deltaTime);
        anim.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        anim.SetBool("IsAttacking", attackInput);
        anim.SetBool("IsDashing", dashInput);
    }
}