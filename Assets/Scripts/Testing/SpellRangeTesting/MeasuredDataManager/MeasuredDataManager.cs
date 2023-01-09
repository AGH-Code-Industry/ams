using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeasuredDataManager : MonoBehaviour {
   [SerializeField] private GameObject measuredDataDisplayPrefab;

   public static MeasuredDataManager instance;
   public List<MeasuredData> measuredDataList = new List<MeasuredData>();

   // Start is called before the first frame update
   void Start() {
      instance = this;
   }

   public MeasuredData StartMeasure(Vector3 startPos) {
      MeasuredData currentMeasure = new MeasuredData(new TimeSpan(DateTime.Now.Ticks).TotalSeconds, startPos);
      return currentMeasure;
   }

   public void StopMeasure(Vector3 endPos, MeasuredData currentMeasure) {
      currentMeasure.endPosition = endPos;
      currentMeasure.endTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;

      currentMeasure.distance = Math.Sqrt(Math.Pow(currentMeasure.startPosition.x - endPos.x, 2) + Math.Pow(currentMeasure.startPosition.z - endPos.z, 2));
      currentMeasure.time = currentMeasure.endTime - currentMeasure.startTime;

      measuredDataList.Add(currentMeasure);

      SpawnMeasuredDataDisplay(currentMeasure);

      string jsonString = JsonUtility.ToJson(currentMeasure);
      Debug.Log(jsonString);
   }

   private void SpawnMeasuredDataDisplay(MeasuredData currentMeasure){
      GameObject measuredDataDisplay = Instantiate(measuredDataDisplayPrefab, currentMeasure.endPosition, Quaternion.identity);

      measuredDataDisplay.transform.parent = this.gameObject.transform;
      var measuredScript = measuredDataDisplay.GetComponent<MeasuredDataDisplay>();

      measuredScript.AddTextValues(currentMeasure.time, currentMeasure.distance, "Test");
   }
}
