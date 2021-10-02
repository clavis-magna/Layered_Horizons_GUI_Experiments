using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showSliderValue : MonoBehaviour
{

    public SliderComponent sliderInput;

    TextMeshProUGUI textDisplay;
    public Slider slider;


    //void Start()
    //{


    //    slider = GetComponentInChildren<Slider>();
    //}


    void Update()
    {
        slider.value = sliderInput.value;

        textDisplay = GetComponent<TextMeshProUGUI>();

        textDisplay.text = sliderInput.sliderName + " : " + sliderInput.value;

    }
}
