using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour {
   [SerializeField] private float _bulletSpeed;

   public MeasuredData currentMeasure;

   private void Start(){
      currentMeasure = MeasuredDataManager.instance.StartMeasure(transform.position);
   }

   // Update is called once per frame
   void Update() {
      transform.position += transform.rotation * new Vector3(0, 0, 1) * _bulletSpeed * Time.deltaTime;

      if (new TimeSpan(DateTime.Now.Ticks).TotalSeconds - currentMeasure.startTime > 1) {
         MeasuredDataManager.instance.StopMeasure(transform.position, currentMeasure);
         Destroy(this.gameObject);
      }
   }
}
