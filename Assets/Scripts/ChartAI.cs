using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float detectionRange = 30f;
    public float stoppingDistance = 2f;

    private Renderer objectRenderer;

    private bool isChasing = false;
    public Vector3 nestPosition;


    void Start() {
        objectRenderer = GetComponent<Renderer>();
        nestPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && distanceToPlayer > stoppingDistance)
        {
            isChasing = true;
            objectRenderer.material.color = Color.yellow;
        }
        else
        {
            isChasing = false;
            objectRenderer.material.color = Color.green;

        }

        if(distanceToPlayer <= detectionRange/2) {
           moveSpeed = 4f;
            objectRenderer.material.color = Color.red;
        } else if (distanceToPlayer > 10 && distanceToPlayer < detectionRange) {
            moveSpeed = 2f;
            objectRenderer.material.color = Color.yellow;

        }




        if (isChasing)
        {
            // Obliczamy kierunek, w ktorym powinien poruszac sie przeciwnik
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            transform.LookAt(player);


            // Przeciwnik porusza sie w kierunku gracza
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (distanceToPlayer < stoppingDistance)
            {
                transform.position = transform.position;
            }

        } else if (!isChasing && distanceToPlayer > stoppingDistance) {

            
            if (Vector3.Distance(transform.position, nestPosition) > 1) {

            objectRenderer.material.color = Color.blue;
            Vector3 directionToNest = nestPosition - transform.position;

            transform.LookAt(nestPosition);
            transform.Translate(directionToNest.normalized * moveSpeed * Time.deltaTime);
            }
            
        }
    }
}
