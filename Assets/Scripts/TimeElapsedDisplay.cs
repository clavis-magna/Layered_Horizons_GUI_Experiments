using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeElapsedDisplay : MonoBehaviour
{

    //Attach the text object here
    public Text display;

    void Update()
    {
        string displayText = "TIME: " + Time.time;
        display.text = displayText;
    }
}
