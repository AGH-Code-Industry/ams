using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

[ExecuteInEditMode]
public class CameraLookAt : MonoBehaviour {
    [SerializeField] GameObject? target;

    void Update() {
        if(target != null) {
            transform.LookAt(target.transform);
        }
    }
}
