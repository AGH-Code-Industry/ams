using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
