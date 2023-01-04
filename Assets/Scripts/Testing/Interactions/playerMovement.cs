using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    PlayerFeatures playerFeatures;
    Rigidbody rb;
    // Start is called before the first frame update
    

    void Start()
    {
        playerFeatures = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFeatures>();
        playerFeatures.speed.baseValue = 5;
        playerFeatures.speed.value = playerFeatures.speed.baseValue;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
    public void Move()
    {

        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.position += Movement * playerFeatures.speed.value * Time.deltaTime;

    }

    public void Rotate()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }
}
