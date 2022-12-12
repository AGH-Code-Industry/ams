using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
   [SerializeField] private float moveSpeed;
   [SerializeField] private float rotationSpeed;

   // Update is called once per frame
   void Update()
   {
      float horizontalInputAxis = Input.GetAxis("Horizontal");
      float verticalInputAxis = Input.GetAxis("Vertical");
      float fire = Input.GetAxis("Jump");

      if (horizontalInputAxis != 0)
      {
         Quaternion rotationEuler = Quaternion.Euler(
            transform.rotation.x,
            transform.rotation.y + horizontalInputAxis * rotationSpeed * Time.deltaTime,
            transform.rotation.z
         );
      
         transform.eulerAngles += new Vector3(0,horizontalInputAxis * rotationSpeed * Time.deltaTime,0);
      }

      if (verticalInputAxis != 0)
      {
         transform.position +=  transform.rotation * new Vector3(0, 0, 1)* verticalInputAxis  * moveSpeed * Time.deltaTime;
      }

      if (fire != 0)
      {
         BulletManager.instance.SpawnBullet(transform.position, transform.rotation);
      }
   }
}
