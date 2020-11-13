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
        masterSlider.value = PlayerPrefs.GetFloat("VolumeMasterParam", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("VolumeMusicParam", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("VolumeSFXParam", 0.75f);

    }
    public void SetLevelMaster(float sliderValue)
    {
        float masterValue = masterSlider.value;
        mixer.SetFloat("VolumeMasterParam", Mathf.Log10(masterValue) * 20);
        PlayerPrefs.SetFloat("VolumeMasterParam", masterValue);
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
