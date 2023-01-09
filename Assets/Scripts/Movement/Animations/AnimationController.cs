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
    private Animator anim;

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
        
        moveDir = new Vector3(horizontalInput, 0f,verticalInput);  
        mousePos = Input.mousePosition;
        screenPos = mainCam.WorldToScreenPoint(target.position);
        mouseOffset = (mousePos - screenPos).normalized;
        mouseOffset = new Vector3(mouseOffset.x, 0, mouseOffset.y);
        float angle = Vector3.SignedAngle(moveDir, mouseOffset, Vector3.up);

        
        Debug.Log(angle);
        velocityX = -Mathf.Sin(angle * Mathf.Deg2Rad) * moveDir.magnitude;    
        velocityY = Mathf.Cos(angle * Mathf.Deg2Rad) * moveDir.magnitude;
        
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

    }
}