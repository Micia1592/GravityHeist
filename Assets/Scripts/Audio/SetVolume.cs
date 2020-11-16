using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider; 
    

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("VolumeMasterParam", 1f); //Get the level at the start
        musicSlider.value = PlayerPrefs.GetFloat("VolumeMusicParam", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("VolumeSFXParam", 1f);

    }
    public void SetLevelMaster(float sliderValue)
    {
        float masterValue = masterSlider.value; //initialise
        mixer.SetFloat("VolumeMasterParam", Mathf.Log10(masterValue) * 20); //set the mixers lvel
        PlayerPrefs.SetFloat("VolumeMasterParam", masterValue); //change in playerprefs so we can call this back
    }

    public void SetLevelMusic(float sliderValue)
    {
        float musicValue = musicSlider.value;
        mixer.SetFloat("VolumeMusicParam", Mathf.Log10(musicValue) * 20);
        PlayerPrefs.SetFloat("VolumeMuicParam", musicValue);
    }

    public void SetLevelSFX(float sliderValue)
    {
        float sfxValue = sfxSlider.value;
        mixer.SetFloat("VolumeSFXParam", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("VolumeSFXParam", sfxValue);
    }
}
