using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public TMP_Dropdown resolution;
    public TMP_Dropdown quality;
    public TMP_Dropdown texture;
    public TMP_Dropdown aa;
    public Slider volumeSlider;
    public AudioSource audioSource;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolution.ClearOptions();

        List<string> options = new();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)

                currentResolutionIndex = i;
        }

        resolution.AddOptions(options);
        resolution.value = currentResolutionIndex;
        resolution.RefreshShownValue();

        LoadSettings(currentResolutionIndex);
    }

    void OnEnable()
    {
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(volumeSlider.value); });
    }

    void ChangeVolume(float sliderValue)
    {
        audioSource.volume = sliderValue;

        PlayerPrefs.SetFloat("VolumePreference", audioSource.volume);
    }

    void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveAllListeners();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        //PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        quality.value = qualityIndex;

        PlayerPrefs.SetInt("QualitySettingPreference", qualityIndex);
    }
    /*
    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;

        texture.value = textureIndex;

        PlayerPrefs.SetInt("TextureQualityPreference", textureIndex);
    }
    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;

        aa.value = aaIndex;

        PlayerPrefs.SetInt("AntiAliasingPreference", aaIndex);
    }
    */
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("VolumePreference"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        }
        else
        {
            volumeSlider.value = 0.75f;
        }

        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            resolution.value = PlayerPrefs.GetInt("ResolutionPreference");
        }
        else
        {
            resolution.value = currentResolutionIndex;
        }

        if (PlayerPrefs.HasKey("FullscreenPreference"))
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        }
        else
        {
            Screen.fullScreen = true;
        }

        if (PlayerPrefs.HasKey("QualitySettingPreference"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualitySettingPreference"));
            quality.value = PlayerPrefs.GetInt("QualitySettingPreference");
        }
        else
        {
            quality.value = 3;
        }
        /*
        if (PlayerPrefs.HasKey("TextureQualityPreference"))
        {
            texture.value = PlayerPrefs.GetInt("TextureQualityPreference");
        }
        else
        {
            texture.value = 3;
        }

        if (PlayerPrefs.HasKey("AntiAliasingPreference"))
        {
            aa.value = PlayerPrefs.GetInt("AntiAliasingPreference");
        }
        else
        {
            aa.value = 3;
        }*/
    }
}