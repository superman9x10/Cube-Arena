using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolController : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        loadSliderValue();
    }
    void loadSliderValue()
    {
        slider.value = PlayerPrefs.GetFloat("SliderValue");
    }
}
