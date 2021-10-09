using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showToggleValue : MonoBehaviour
{
    public ToggleComponent toggleInput;

    TextMeshProUGUI textDisplay;

    void Update()
    {
        bool active = toggleInput.active;

        textDisplay = GetComponent<TextMeshProUGUI>();

        if (active)
        {
            textDisplay.text = toggleInput.toggleName + " : " + toggleInput.toggleOnString;
        } else
        {
            textDisplay.text = toggleInput.toggleName + " : " + toggleInput.toggleOffString;
        }


    }
}
