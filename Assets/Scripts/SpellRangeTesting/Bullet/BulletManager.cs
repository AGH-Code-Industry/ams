using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
   [SerializeField] private GameObject BulletPrefab;
   private double lastTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
   public static BulletManager instance;

   // Start is called before the first frame update
   void Start()
   {
      instance = this;
   }

   public void SpawnBullet(Vector3 spawnPosition, Quaternion spawnRotation)
   {
      if (new TimeSpan(DateTime.Now.Ticks).TotalSeconds - lastTime < 1) return;
      lastTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;

      GameObject spawnedBullet = Instantiate(BulletPrefab, spawnPosition + new Vector3(0, 1.35f, 0), spawnRotation);
      spawnedBullet.transform.parent = transform;
   }
}
