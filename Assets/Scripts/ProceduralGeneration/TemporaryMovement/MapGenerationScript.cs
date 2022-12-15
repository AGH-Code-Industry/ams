using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> _rooms;
    [SerializeField] private List<GameObject> _floors;
    private MeshFilter _mesh;

    [Header("Variables")]
    [SerializeField] private float _worldScale;
    [SerializeField] private float _roomAmout;
    private int _roomIndex;
    private Vector3 _floorSize;


    private void Awake()
    {
        AddRoomPrefabs();
        RoomPlacement();
    }

    private void Update()
    {
        CheckingIfOverlapping();
    }



    private void AddRoomPrefabs()
    {
        foreach(GameObject room in Resources.LoadAll<GameObject>("RoomPrefabs")) //Wa¿ne, aby dodawaæ ka¿dy prefab do folderu o œcie¿ce Assets/Resources/RoomPrefabs
        {
            _rooms.Add(room);
            room.transform.position = new Vector3(0, 0, 0);
        }
    }

    private void RoomPlacement()
    {
        for(int i = 0; i < _roomAmout; i++)
        {
            _roomIndex = Random.Range(0, _rooms.Count);
            Instantiate(_rooms[_roomIndex], new Vector3(Random.Range(0, _worldScale), 0, Random.Range(0, _worldScale)), Quaternion.Euler(0, Random.Range(0, 3) * 90, 0));           
        }

        foreach (GameObject floor in GameObject.FindGameObjectsWithTag("Floor"))
        {
            _floors.Add(floor);
        }
    }

    private void CheckingIfOverlapping()
    {


        for (int i = 0; i < _floors.Count; i++)
        {
            for (int p = 0; p < _floors.Count; p++)
            {
                if (i != p)
                {
                    if (_floors[i].GetComponent<MeshCollider>().bounds.Intersects(_floors[p].GetComponent<MeshCollider>().bounds))
                    {
                        _floors[i].transform.parent.position = new Vector3(Random.Range(0, _worldScale), 0, Random.Range(0, _worldScale));
                    }
                }
            }
        }
    }
}
