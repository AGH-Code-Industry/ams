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
    float startTime;

    private void Start() {
        startPosition = transform.position;
        startTime = Time.time;
    }

    private void Update() {
        float time = (Time.time - startTime) * (MathF.PI/8);
        
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
