using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("VolumeMasterParam", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelMusic(float sliderValue)
    {
        mixer.SetFloat("VolumeMusicParam", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelSFX(float sliderValue)
    {
        mixer.SetFloat("VolumeSFXParam", Mathf.Log10(sliderValue) * 20);
    }
}
