using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleList : MonoBehaviour
{
    public Text display;
    public Toggle toggle1Input;
    public Image toggle1Output;

    public Toggle toggleTwo;


    // Update is called once per frame
    void Update()
    {
        if (toggle1Input.active)
        {
            toggle1Output.GetComponent<Image>().color = Color.black;
        } else
        {
            toggle1Output.GetComponent<Image>().color = Color.blue;

        }

        display.text = toggle1Input.toggleName + " – " + toggle1Input.active + "\n" + toggleTwo.toggleName + "–" + toggleTwo.active;
    }
}
