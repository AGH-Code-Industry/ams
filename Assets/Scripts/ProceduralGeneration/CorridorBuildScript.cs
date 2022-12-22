using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorBuildScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> _corridorFloors;
    [SerializeField] private List<GameObject> _walls;

    private void Awake()
    {
        foreach(GameObject floor in GameObject.FindGameObjectsWithTag("CorridorFloor"))
        {
            _corridorFloors.Add(floor);
        }
    }

    private void Start()
    {
        DestroyOverlappingWalls();
        DestroyIfDoor();
    }


    private void DestroyOverlappingWalls()
    {
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            _walls.Add(wall);
        }

        for (int i = 0; i < _walls.Count; i++)
        {
            for (int p = 0; p < _walls.Count; p++)
            {
                if (i != p)
                {
                    if (_walls[i].transform.position.normalized == _walls[p].transform.position.normalized)
                    {
                        Destroy(_walls[i].gameObject);
                        Destroy(_walls[p].gameObject);
                    }
                }
            }
        }
    }

    private void DestroyIfDoor()
    {
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        { 
            foreach(Collider i in Physics.OverlapBox(wall.transform.position, new Vector3(1, 1, 1), Quaternion.Euler(0, 0, 0))) //TODO: layerMask
            {
                if(i.gameObject.CompareTag("Door"))
                {
                    Destroy(wall);
                }
            }
        }
    }
}
