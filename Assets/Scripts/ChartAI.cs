using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float detectionRange = 30f;
    public float stoppingDistance = 2f;
    private float firstAcceleration = 0f;

    private Renderer objectRenderer;

    private bool isChasing = false;

    void Start() {
        objectRenderer = GetComponent<Renderer>();
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
            // Obliczamy kierunek, w którym powinien poruszać się przeciwnik
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Obracamy przeciwnika w kierunku gracza
            transform.LookAt(player);

            // if (distanceToPlayer < stoppingDistance) {
            //     moveSpeed = 0f;
            // }

            // Przeciwnik porusza się w kierunku gracza
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (distanceToPlayer < stoppingDistance)
            {
                transform.position = transform.position;
            }
        }
    }
}
