using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class romperaMovement : MonoBehaviour
{
    public Vector3 startingPosition;
    public Transform player;
    public GameObject chartWatykanski;

    [SerializeField] private Transform wall1;
    [SerializeField] private Transform wall2;
    [SerializeField] private Transform wall3;
    [SerializeField] private Transform wall4;

    public float speed = 3f;
    public float movRange = 16f;
    private float periodCount = -0.5f;
    private int TPcounter = 1;
    private bool isMovingLeft = true;


    void Start() {
        startingPosition = transform.position;    
    }


    void Update() {

        if (Time.time - periodCount >= 5) {
            Teleportation();
            periodCount = Time.time; 
        }


        if (TPcounter % 4 == 1 || TPcounter % 4 == 2) {

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
        } else  if (TPcounter % 4 == 3 || TPcounter % 4 == 0) {


            if (isMovingLeft) {
                transform.Translate(Vector3.right * speed * Time.deltaTime);

                if (Mathf.Abs(wall3.position.z - transform.position.z) >= movRange) {
                    isMovingLeft = false;
                }
            }
            else {
                transform.Translate(Vector3.left * speed * Time.deltaTime);

                if (Mathf.Abs(wall4.position.z - transform.position.z) >= movRange) {
                    isMovingLeft = true;
                }
            }

        }

            if(Mathf.Abs(transform.position.x - startingPosition.x) <= 0.1f) {
                periodCount += 0.5f;
            }

    }


    private void chartSpawn() {
        Instantiate(chartWatykanski, transform.position, Quaternion.identity);
    }

    private void Teleportation() {


        if (TPcounter % 4 == 1) {
            Vector3 newPosition = new Vector3(wall2.position.x, transform.position.y, wall2.position.z + 1f);
            transform.position = newPosition;
            transform.Rotate(0f, 180f, 0f);
            // chartSpawn();
        }
        else if (TPcounter % 4 == 2) {
            
            Vector3 newPosition2 = new Vector3(wall3.position.x + 1f, transform.position.y, wall3.position.z - 1f);
            transform.position = newPosition2;
            transform.Rotate(0f, 90f, 0f);
            // chartSpawn();
            // chartSpawn();
        
        }
        else if (TPcounter % 4 == 3) {
            
            Vector3 newPosition3 = new Vector3(wall4.position.x - 1f, transform.position.y, wall4.position.z);
            transform.position = newPosition3;
            transform.Rotate(0f, 180f, 0f);
            // chartSpawn();
        
        }
        else if (TPcounter % 4 == 0) {
            
            Vector3 newPosition4 = new Vector3(wall1.position.x , transform.position.y, wall1.position.z - 1f);
            transform.position = newPosition4;
            transform.Rotate(0f, 270f, 0f);
            // chartSpawn();
            // chartSpawn();

        }

        TPcounter += 1;

    }

}
