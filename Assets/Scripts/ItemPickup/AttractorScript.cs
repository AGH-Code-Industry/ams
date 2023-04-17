using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorScript : MonoBehaviour
{

    public float attractorSpeed = 10f;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, attractorSpeed * Time.deltaTime);
        }
    }

    private void Update() 
    {
        if(transform.childCount < 1)
        {
            Destroy(gameObject);
        }
    }

}
