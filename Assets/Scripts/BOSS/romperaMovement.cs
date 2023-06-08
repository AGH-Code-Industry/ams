using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class romperaMovement : MonoBehaviour
{
    private Renderer objectRenderer;
    public Vector3 startingPosition;
    public Transform player;
    public float speed = 3f;
    public float movRange = 16f;
    private bool isMovingLeft = true;
    private float periodCount = -0.5f;
    private Vector3 positionBeforeTeleportaion;

    // Start is called before the first frame update
    void Start() {
        objectRenderer = GetComponent<Renderer>();
        startingPosition = transform.position;    
    }


    // Update is called once per frame
    void Update() {

        if (Time.time - periodCount >= 10) {
            Teleportation();
            periodCount = Time.time; 
        }


        if (isMovingLeft) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (Mathf.Abs(startingPosition.x - transform.position.x) >= movRange) {
                isMovingLeft = false;
            }
        }
        else {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (Mathf.Abs(startingPosition.x - transform.position.x) >= movRange) {
                isMovingLeft = true;
            }
        }



        if(Mathf.Abs(transform.position.x - startingPosition.x) <= 0.1f) {
            periodCount += 0.5f;
        }

    }

    private void Teleportation() {
        positionBeforeTeleportaion = transform.position;
        positionBeforeTeleportaion.z += 38f;
        transform.Translate(transform.position - positionBeforeTeleportaion);
        transform.Rotate(0f, 180f, 0f);

    }
}
