using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> _rooms;
    [SerializeField] private List<GameObject> _floors;

    [Header("Variables")]
    [SerializeField] private int _worldScale;
    [SerializeField] private float _roomAmout;
    [SerializeField] private float _corridorSize;
    [SerializeField] private float _padding;
    private int _roomIndex;


    private void Awake()
    {
        AddRoomPrefabs();
        RoomPlacement();
    }

    private void Start()
    {
        IsOverlapping();
    }

    private void Update()
    {
        IsOverlapping();
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
            Instantiate(_rooms[_roomIndex], Vector3.zero, Quaternion.Euler(0, Random.Range(0, 3) * 90, 0));           
        }

        foreach (GameObject floor in GameObject.FindGameObjectsWithTag("Floor"))
        {
            _floors.Add(floor);
        }
    }

    private void IsOverlapping()
    {

        //bool roomWasRerolled = true;

       // while(roomWasRerolled)
       // {
            //roomWasRerolled = false;

            foreach (GameObject floor in _floors)
            {
                Collider[] hit = Physics.OverlapBox(floor.transform.position, (floor.GetComponent<MeshRenderer>().bounds.size) / 2 + new Vector3(_padding, 0 , _padding), floor.transform.rotation);

                foreach (Collider roomObject in hit)
                {
                    if (!roomObject.gameObject.CompareTag("Floor"))
                    {
                        continue;
                    }

                    if (roomObject.transform == floor.transform)
                    {
                        continue;
                    }

                    RerollPosition(floor);
                    //roomWasRerolled = true;
                }
            }
       // }
    }

    private void RerollPosition(GameObject floor)
    {
        MeshRenderer roomSize = floor.GetComponent<MeshRenderer>();

        float x = GenerateAxisPosition(roomSize.bounds.size.x);
        float z = GenerateAxisPosition(roomSize.bounds.size.z);

        floor.transform.parent.rotation = GenerateAxisRotation();

        if(floor.transform.parent.rotation.y % 180 != 0)
        {
            x += _corridorSize / 2;
            z += _corridorSize / 2;
        }

        floor.transform.parent.position = new Vector3(x, 0, z);
    }

    private float GenerateAxisPosition(float roomAxisSize)
    {
        float x = Mathf.RoundToInt(Random.Range(0, _worldScale / _corridorSize));

        if(Mathf.RoundToInt(roomAxisSize) % (_corridorSize * 2) != 0)
        {
            x += _corridorSize / 2;
        }

        return x * _corridorSize;
    }

    private Quaternion GenerateAxisRotation()
    {
        return Quaternion.Euler(0, Random.Range(0, 3) * 90, 0);      
    }

    private void OnDrawGizmos()
    {

        foreach (GameObject floor in _floors)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(floor.transform.position, (floor.GetComponent<MeshRenderer>().bounds.size) + new Vector3(_padding, 0, _padding));
        }
    }
}
