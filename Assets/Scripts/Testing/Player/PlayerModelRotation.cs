using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelRotation : MonoBehaviour {

    [SerializeField] private Transform _playerModel;

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 1000)) {
            Vector3 dir = hit.point - transform.position;
            _playerModel.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z), Vector3.up);
        }
    }
}
