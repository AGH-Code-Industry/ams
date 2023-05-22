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
    private int selectedRes;
    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI ParticlesText;
    public TextMeshProUGUI graphicText;
    public TextMeshProUGUI SensivityText;
    public Slider sensitivitySlider;

    void Start()
    {
        fullscreenToggle.isOn = Settings.fullscreen;

        volumeSlider.GetComponent<Slider>().value = Settings.volume;

        selectedRes = Settings.resolutionPreset;
        resolutionText.text = Settings.resolutions[selectedRes].horizontal.ToString() + "x" + Settings.resolutions[selectedRes].vertical.ToString();
        Screen.SetResolution(Settings.resolutions[selectedRes].horizontal, Settings.resolutions[selectedRes].vertical, fullscreenToggle.isOn);

        ParticlesApply();
        // Wł/Wył cząsteczki

        GraphicApply();

        sensitivitySlider.GetComponent<Slider>().value =  Settings.sensitivity;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Settings.fullscreen = isFullscreen;
    }

    public void Volume(float value) {
        Settings.volume = value;
        volumeText.text = value.ToString();
        // [Głośność] = value*[GłośnośćMax]/10
    }

    public void Resolution() {
        selectedRes++;
        if(selectedRes >= Settings.resolutions.Count) {
            selectedRes = 0;
            }
        resolutionText.text = Settings.resolutions[selectedRes].horizontal.ToString() + "x" + Settings.resolutions[selectedRes].vertical.ToString();
    }

    public void ApplyResolution() {
        Settings.resolutionPreset = selectedRes;
    }

    public void Particles() {
        Settings.particlesOn = !Settings.particlesOn;
        ParticlesApply();
        // Wł/Wył cząsteczki. Dodać to samo w metodzie Start()
    }
    
    public void ParticlesApply() {

        switch(Settings.particlesOn) {
            case true:
                ParticlesText.text = "On";
                break;
            default:
                ParticlesText.text = "Off";
                break;
        }
    }

    public void Graphics() {
        Settings.graphicQuality = (Settings.graphicQuality + 1) % 3;
        GraphicApply();
    }
    public void GraphicApply() {
        switch(Settings.graphicQuality) {
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
    }

     public void Sensivity(float value) {
        Settings.sensitivity = value;
        SensivityText.text = value.ToString();
        // [Czułość] = value*[CzułośćMax]/100
    }

    public void KeyMap() {
        SceneManager.LoadScene("ControlsSettings");
    }

    public void Back() {
        SceneManager.LoadScene("MenuStart");
    }
}