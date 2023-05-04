using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biting : MonoBehaviour
{
    private Renderer objectRenderer;
    public Transform player;
    float biteCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.red;

        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);


        if (distance <= 1.5f) {
            biteCounter += 1;
            if (biteCounter % 100 == 0) {
                objectRenderer.material.color = Color.black;
            } else {
            objectRenderer.material.color = Color.red;
            }
        } else {
            objectRenderer.material.color = Color.red;

        }
        
    }
}
