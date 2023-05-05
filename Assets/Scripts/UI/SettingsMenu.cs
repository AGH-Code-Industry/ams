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
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedRes;
    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI ParticlesText;
    public TextMeshProUGUI graphicText;
    public TextMeshProUGUI SensivityText;
    public Slider sensitivitySlider;

    void Start()
    {
        
        bool isFullscreen = PlayerPrefs.GetInt("isFullscreen", 0) == 1 ? true : false;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;

        Settings.instance.volumeValue_ = PlayerPrefs.GetFloat("Volume", 10);
        volumeSlider.GetComponent<Slider>().value = Settings.instance.volumeValue_;

        selectedRes = PlayerPrefs.GetInt("Resolution", 0);
        resolutionText.text = resolutions[selectedRes].horizontal.ToString() + "x" + resolutions[selectedRes].vertical.ToString();
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreenToggle.isOn);

        Settings.instance.particlesOn_ = (PlayerPrefs.GetInt("AreParticlesOn", 1) == 1 ? true : false);
        ParticlesApply();
        // Wł/Wył cząsteczki

        Settings.instance.graphicQuality_ = PlayerPrefs.GetInt("Graphics", 2);
        GraphicApply();

        Settings.instance.sensitivityValue_ = PlayerPrefs.GetFloat("Sensivity", 50);
        sensitivitySlider.GetComponent<Slider>().value =  Settings.instance.sensitivityValue_;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("isFullscreen", isFullscreen ? 1 : 0);
    }

    public void Volume(float value) {
        Settings.instance.volumeValue_ = value;
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
        Settings.instance.particlesOn_ = !Settings.instance.particlesOn_;
        ParticlesApply();
        // Wł/Wył cząsteczki. Dodać to samo w metodzie Start()
    }
    
    public void ParticlesApply() {

        PlayerPrefs.SetInt("AreParticlesOn", Settings.instance.particlesOn_ == true ? 1 : 0);
        switch(Settings.instance.particlesOn_) {
            case true:
                ParticlesText.text = "On";
                break;
            default:
                ParticlesText.text = "Off";
                break;
        }
    }

    public void Graphics() {
        Settings.instance.graphicQuality_++;
        if(Settings.instance.graphicQuality_ > 2) {
            Settings.instance.graphicQuality_ = 0;
            }
        GraphicApply();
    }
    public void GraphicApply() {
        switch(Settings.instance.graphicQuality_) {
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
        PlayerPrefs.SetInt("Graphics", Settings.instance.graphicQuality_);
    }

     public void Sensivity(float value) {
        Settings.instance.sensitivityValue_ = value;
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