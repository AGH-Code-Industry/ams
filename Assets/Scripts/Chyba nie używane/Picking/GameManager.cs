using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<string> inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray,out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "Pickable")
                {
                    inventory.Add(hitInfo.collider.gameObject.name);
                    Debug.Log(hitInfo.collider.gameObject.name + "Picked !");
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }
    }
}
