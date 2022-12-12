using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeasuredData
{
   public double time;
   public double distance;

   public double startTime;
   public double endTime;

   public Vector3 startPosition;
   public Vector3 endPosition;

   public MeasuredData(
      double startTime,
      Vector3 startPosition
   )
   {
      this.startTime =startTime;
      this.startPosition = startPosition;
   }
}