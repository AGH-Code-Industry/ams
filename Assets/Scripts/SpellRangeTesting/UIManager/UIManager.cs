using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
   [SerializeField] TMP_Text timeText;
   [SerializeField] TMP_Text distanceText;

   public static UIManager instance;
   public List<MeasuredData> measuredDataList = new List<MeasuredData>();

   // Start is called before the first frame update
   void Start()
   {
      instance = this;
   }

   public MeasuredData StartMeasure(Vector3 startPos)
   {
      MeasuredData currentMeasure = new MeasuredData(new TimeSpan(DateTime.Now.Ticks).TotalSeconds, startPos);
      return currentMeasure;
   }

   public void StopMeasure(Vector3 endPos, MeasuredData currentMeasure)
   {
      currentMeasure.endPosition = endPos;
      currentMeasure.endTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;

      currentMeasure.distance = Math.Sqrt(Math.Pow(currentMeasure.startPosition.x - endPos.x, 2) + Math.Pow(currentMeasure.startPosition.z - endPos.z, 2));
      currentMeasure.time = currentMeasure.endTime - currentMeasure.startTime;

      measuredDataList.Add(currentMeasure);

      timeText.text = $"New Time: {currentMeasure.time:0.##}s";
      distanceText.text = $"New Distance: {currentMeasure.distance:0.##}";

      string jsonString = JsonUtility.ToJson(currentMeasure);
      Debug.Log(jsonString);
   }
}
