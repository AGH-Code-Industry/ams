using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float _movementSpeed = 6f;

    void Update() {
        Vector2 moveVector = InputManager.actions.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);

        if(direction.sqrMagnitude > 1) {
            direction = direction.normalized;
        }

        Vector3 movement = _movementSpeed * Time.deltaTime * direction;

        transform.position = transform.position + movement;
    }
}
