using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// access to settings by: Settings.instance.chosenValue
// chosenValue: choose from class below

public class Settings : MonoBehaviour
{
    public static int defaultFullscreen = 1;    
    public static float defaultVolume = 5;
    public static int defaultResolutionPreset = 4;
    public static int defaultGraphicQuality = 3;
    public static float defaultSensitivity = 50;
    public static int defaultParticlesOn = 0; 

    public class ResItem {
    public int horizontal, vertical;
    }

    public static List<ResItem> resolutions = new List<ResItem>() {
        new ResItem() { horizontal = 800, vertical = 600 },
        new ResItem() { horizontal = 1280, vertical = 720 },
        new ResItem() { horizontal = 1366, vertical = 768 },
        new ResItem() { horizontal = 1600, vertical = 900 },
        new ResItem() { horizontal = 1920, vertical = 1080 },
        new ResItem() { horizontal = 2560, vertical = 1440 },
        new ResItem() { horizontal = 3840, vertical = 2160 }
    };

    public static bool fullscreen {
        get { return PlayerPrefs.GetInt("isFullscreen", defaultFullscreen) == 1 ? true : false; }
        set {
            PlayerPrefs.SetInt("isFullscreen", value ? 1 : 0);             
            Screen.fullScreen = value;
            }
    }

    public static float volume {
        get { return PlayerPrefs.GetFloat("volume", defaultVolume); }
        set { PlayerPrefs.SetFloat("volume", value); }
    }

    public static int resolutionPreset {
        get { return PlayerPrefs.GetInt("resolutionPreset", defaultResolutionPreset); }
        set {
            PlayerPrefs.SetInt("resolutionPreset", value);
            Screen.SetResolution(resolutions[value].horizontal, resolutions[value].vertical, fullscreen);
            }
    }

    public static int graphicQuality {
        get { return PlayerPrefs.GetInt("graphicQuality", defaultGraphicQuality); }
        set {
            PlayerPrefs.SetInt("graphicQuality", value);
            QualitySettings.SetQualityLevel(value);
            }
    }

    public static float sensitivity {
        get { return PlayerPrefs.GetFloat("sensitivity", defaultSensitivity); }
        set { PlayerPrefs.SetFloat("sensitivity", value); }
    }

    public static bool particlesOn {
        get { return PlayerPrefs.GetInt("particlesOn", defaultParticlesOn) == 1 ? true : false; }
        set { PlayerPrefs.SetInt("particlesOn", value ? 1 : 0);
        }
    }

    public static void ApplySettings() {
        Screen.fullScreen = fullscreen;
        Screen.SetResolution(resolutions[resolutionPreset].horizontal, resolutions[resolutionPreset].vertical, fullscreen);
        QualitySettings.SetQualityLevel(graphicQuality);
    }

    public static void ResetSettings()
    {
        fullscreen = defaultFullscreen == 1;
        volume = defaultVolume;
        resolutionPreset = defaultResolutionPreset;
        graphicQuality = defaultGraphicQuality;
        sensitivity = defaultSensitivity;
        particlesOn = defaultParticlesOn == 1;
    }
}