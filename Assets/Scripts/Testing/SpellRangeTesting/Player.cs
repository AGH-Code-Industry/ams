using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
   [SerializeField] private float moveSpeed;
   [SerializeField] private float rotationSpeed;

   private void Start() {
      InputManager.actions.Player.Levitate.started += _ => SpawnBullet();
   }

   private void SpawnBullet() {
      BulletManager.instance.SpawnBullet(transform.position, transform.rotation);
   }

   // Update is called once per frame
   void Update()
   {
      Vector2 moveVector = InputManager.actions.Player.Move.ReadValue<Vector2>();

      if (moveVector.x != 0)
      {
         Quaternion rotationEuler = Quaternion.Euler(
            transform.rotation.x,
            transform.rotation.y + moveVector.x * rotationSpeed * Time.deltaTime,
            transform.rotation.z
         );
      
         transform.eulerAngles += new Vector3(0,moveVector.x * rotationSpeed * Time.deltaTime,0);
      }

      if (moveVector.y != 0)
      {
         transform.position +=  transform.rotation * new Vector3(0, 0, 1)* moveVector.y  * moveSpeed * Time.deltaTime;
      }
   }
}
