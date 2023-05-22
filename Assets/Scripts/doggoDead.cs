using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doggoDead : MonoBehaviour
{
    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update() {
 
        objectRenderer.material.color = Color.blue;

    }

    public void DEAD() {
        Destroy(gameObject);
        

    }
}
