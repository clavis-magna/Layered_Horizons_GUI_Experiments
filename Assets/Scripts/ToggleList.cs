using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleList : MonoBehaviour
{
    public Text display;
    public Toggle toggle1Input;
    public Image toggle1Output;

    public Toggle toggle2Input;
    public Image toggle2Output;


    // Update is called once per frame
    void Update()
    {

        //for the outputs, change the image colour
        if (toggle1Input.active)
        {
            toggle1Output.GetComponent<Image>().color = Color.black;
        }
        else
        {
            toggle1Output.GetComponent<Image>().color = Color.blue;
        }



        if (toggle2Input.active)
        {
            toggle2Output.GetComponent<Image>().color = Color.black;
        }
        else
        {
            toggle2Output.GetComponent<Image>().color = Color.blue;
        }



        //assign the text to the toggle text
        display.text = toggle1Input.toggleName + " – " + toggle1Input.active + "\n" + toggle2Input.toggleName + "–" + toggle2Input.active + "\n" + "placeholder third toggle this is a terrible system";
    }
}
