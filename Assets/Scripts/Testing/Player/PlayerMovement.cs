using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] public float _movementSpeed = 6f;

    void Update() {
        float mvX = Input.GetAxis("Horizontal");
        float mvZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(mvX, 0, mvZ);

        if(direction.sqrMagnitude > 1) {
            direction = direction.normalized;
        }

        Vector3 movement = _movementSpeed * Time.deltaTime * direction;

        transform.position = transform.position + movement;
    }
}
