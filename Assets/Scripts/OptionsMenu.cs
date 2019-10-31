using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullScreenTog, vsyncTog;
    public ResItem[] resolution;
    public int selectedResolution;
    public Text resolutionLabel;
    public AudioMixer theMixer;
    public Slider masterSlider, musicSlider, sfxSlider;
    public Text masterLabel, musicLabel, sfxLabel;
    public AudioSource sfxLoop;
    // Start is called before the first frame update
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
            vsyncTog.isOn = false;
        else
            vsyncTog.isOn = true;
        //search for resolution in list
        bool foundRes = false;
        for (int i = 0; i < resolution.Length; i++)
        {
            if (Screen.width == resolution[i].horizontal && Screen.height == resolution[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }
        if (!foundRes)
            resolutionLabel.text = Screen.width.ToString() + " x " + Screen.height.ToString();
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
           
        }
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
           
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
            
        }
        masterLabel.text = (masterSlider.value + 80).ToString();
        musicLabel.text = (musicSlider.value + 80).ToString();
        sfxLabel.text = (sfxSlider.value + 80).ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResLeft()
    {
        selectedResolution++;
        if (selectedResolution > resolution.Length - 1)
            selectedResolution = resolution.Length - 1;
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution--;
        if (selectedResolution < 0)
            selectedResolution = 0;
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolution[selectedResolution].horizontal.ToString() + " x " + resolution[selectedResolution].vertical.ToString();
    }
    public void ApplyGraphic()
    {
        //Screen.fullScreen = fullScreenTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        //set resolution
        Screen.SetResolution(resolution[selectedResolution].horizontal, resolution[selectedResolution].vertical, fullScreenTog.isOn);
    }

    public void SetMasterVol()
    {
        masterLabel.text = (masterSlider.value + 80).ToString();
        theMixer.SetFloat("MasterVol", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void SetMusicVol()
    {
        musicLabel.text = (musicSlider.value + 80).ToString();
        theMixer.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVol()
    {
        sfxLabel.text = (sfxSlider.value + 80).ToString();
        theMixer.SetFloat("SFXVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }

    public void PlaySFXLoop()
    {
        sfxLoop.Play();
    }

    public void StopSFXLoop()
    {
        sfxLoop.Stop();
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}