using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolSetting : MonoBehaviour
{
    public AudioMixer mixer;

    public void setVol(float sliderValue)
    {
        float volValue = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("VolValue", volValue);

        PlayerPrefs.SetFloat("SliderValue", sliderValue);
    }
}
