using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// access to settings by: Settings.instance.chosenValue
// chosenValue: choose from class below

public class Settings : MonoBehaviour
{
    public float volumeValue_;
    public bool particlesOn_;
    public int graphicQuality_;
    public float sensitivityValue_;

    public static Settings instance;
    void Awake() {
        instance = this;
    }
    void Start()
    {

    }

}
