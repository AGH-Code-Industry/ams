using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeasuredDataDisplay : MonoBehaviour {
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text distanceText;
    [SerializeField] TMP_Text spellNameText;

    Vector3 startPosition;
    double startTime;

    private void Start() {
        startPosition = transform.position;
        startTime = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
    }

    private void Update() {
        float time = (float) ((new TimeSpan(DateTime.Now.Ticks).TotalSeconds - startTime) * (MathF.PI/8) % (2*Math.PI));

        Debug.Log(time);
        transform.position = new Vector3(
            startPosition.x,
            startPosition.y + Mathf.Sin(time)*0.4f,
            startPosition.z
        );
    }

    public void AddTextValues(double time, double distance, string spellName) {
        // timeText.text = time.ToString();
        timeText.text = $"Time: {time:0.##}s";
        distanceText.text = $"Distance: {distance:0.##}";
        spellNameText.text = $"Spell: {spellName}";; 
    }
}
