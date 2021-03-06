using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showSliderValue : MonoBehaviour
{
    //also get the gameobject so that we can check if it's active.
    public GameObject toggleObject;

    public SliderComponent sliderInput;

    TextMeshProUGUI textDisplay;

    public Slider slider;


    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        slider.value = sliderInput.value;
        textDisplay.text = sliderInput.sliderName + " : " + sliderInput.value;

        //know if the gameobject is active or not and change the text colour so that we know which one we are changing.
        if (toggleObject.activeSelf)
        {
            textDisplay.color = new Color(1, 1, 1, 1.0f);
        }
        else
        {
            textDisplay.color = new Color(1, 1, 1, 0.1f);
        }
    }
}
