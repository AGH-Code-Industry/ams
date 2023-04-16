using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody rb;
    public int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
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
        Vector2 moveVector = InputManager.actions.Player.Move.ReadValue<Vector2>();
        Vector3 Movement = new Vector3(moveVector.x, 0, moveVector.y);

        transform.position += Movement * speed * Time.deltaTime;

    }

    public void Rotate()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }
}
