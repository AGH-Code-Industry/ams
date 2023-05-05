using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class SettingsMenu : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public TextMeshProUGUI volumeText;
    public Slider volumeSlider;
    private float volumeValue;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedRes;
    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI ParticlesText;
    private int areParticlesOn;
    public TextMeshProUGUI graphicText;
    private int selectedGraphic;
    public TextMeshProUGUI SensivityText;
    public Slider sensitivitySlider;
    private float sensitivityValue;

    void Start()
    {
        bool isFullscreen = PlayerPrefs.GetInt("isFullscreen", 0) == 1 ? true : false;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;

        volumeValue = PlayerPrefs.GetFloat("Volume", 10);
        volumeSlider.GetComponent<Slider>().value = volumeValue;

        selectedRes = PlayerPrefs.GetInt("Resolution", 0);
        resolutionText.text = resolutions[selectedRes].horizontal.ToString() + "x" + resolutions[selectedRes].vertical.ToString();
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreenToggle.isOn);

        areParticlesOn = PlayerPrefs.GetInt("AreParticlesOn", 1);
        ParticlesApply();
        // Wł/Wył cząsteczki

        selectedGraphic = PlayerPrefs.GetInt("Graphics", 2);
        GraphicApply();

        sensitivityValue = PlayerPrefs.GetFloat("Sensivity", 50);
        sensitivitySlider.GetComponent<Slider>().value =  sensitivityValue;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("isFullscreen", isFullscreen ? 1 : 0);
    }

    public void Volume(float value) {
        volumeValue = value;
        volumeText.text = value.ToString();
        PlayerPrefs.SetFloat("Volume", value);
        // [Głośność] = value*[GłośnośćMax]/10
    }

    public void Resolution() {
        selectedRes++;
        if(selectedRes >= resolutions.Count) {
            selectedRes = 0;
            }
        resolutionText.text = resolutions[selectedRes].horizontal.ToString() + "x" + resolutions[selectedRes].vertical.ToString();
    }

    public void ApplyResolution() {
        PlayerPrefs.SetInt("Resolution", selectedRes);
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreenToggle.isOn);
    }

    public void Particles() {
        if(areParticlesOn == 1) {
            areParticlesOn = 0;
        }
        else {
            areParticlesOn = 1;
        }
        ParticlesApply();
        // Wł/Wył cząsteczki. Dodać to samo w metodzie Start()
    }
    
    public void ParticlesApply() {
        PlayerPrefs.SetInt("AreParticlesOn", areParticlesOn);
        switch(areParticlesOn) {
            case 1:
                ParticlesText.text = "On";
                break;
            default:
                ParticlesText.text = "Off";
                break;
        }
    }

    public void Graphics() {
        selectedGraphic++;
        if(selectedGraphic > 2) {
            selectedGraphic = 0;
            }
        GraphicApply();
    }
    public void GraphicApply() {
        switch(selectedGraphic) {
            case 0:
                graphicText.text = "Low";
                // set low
                break;
            case 1:
                graphicText.text = "Medium";
                // set medium
                break;
            case 2:
                graphicText.text = "High";
                // set high
                break;
        }
        PlayerPrefs.SetInt("Graphics", selectedGraphic);
    }

     public void Sensivity(float value) {
        SensivityText.text = value.ToString();
        PlayerPrefs.SetFloat("Sensivity", value);
        // [Czułość] = value*[CzułośćMax]/100
    }

    public void KeyMap() {
        SceneManager.LoadScene("ControlsSettings");
    }

    public void Back() {
        // Go to sceene where you got to settings from
    }
}

[System.Serializable]
public class ResItem {
    public int horizontal, vertical;
}