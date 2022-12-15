using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class FloorScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _wall;

    [Header("Variables")]
    private Vector3 _wallPos;
    private float _wallOffset;
    [SerializeField] private MeshRenderer _mesh;

    private void Awake()
    {
        _wallOffset = _mesh.GetComponent<MeshFilter>().sharedMesh.bounds.extents.x;

        CreateLeft();
        CreateRight();
        CreateForward();
        CreateBack();
    }

    private void CreateLeft()
    {       
        _wallPos = gameObject.transform.TransformPoint(Vector3.left * _wallOffset + Vector3.up * 2);
        Instantiate(_wall, _wallPos, Quaternion.Euler(0, 90, 0), gameObject.transform);
    }

    private void CreateRight()
    {
        _wallPos = gameObject.transform.TransformPoint(Vector3.right * _wallOffset + Vector3.up * 2);
        Instantiate(_wall, _wallPos, Quaternion.Euler(0, 90, 0), gameObject.transform);
    }

    private void CreateForward()
    {
        _wallPos = gameObject.transform.TransformPoint(Vector3.forward * _wallOffset + Vector3.up * 2);
        Instantiate(_wall, _wallPos, Quaternion.Euler(0, 0, 0), gameObject.transform);
    }

    private void CreateBack()
    {
        _wallPos = gameObject.transform.TransformPoint(Vector3.back * _wallOffset + Vector3.up * 2);
        Instantiate(_wall, _wallPos, Quaternion.Euler(0, 0, 0), gameObject.transform);
    }
}
