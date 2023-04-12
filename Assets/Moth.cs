using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : MonoBehaviour
{
    public float damage = 10f;
    //private PlayerHealth playerHealth; (for the future)

    private void Awake()
    {
        //playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>(); (for the future)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //playerHealth.TakeDamage(damage); (for the future)
            Destroy(gameObject);
        }
    }
}
