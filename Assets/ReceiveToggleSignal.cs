using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script reads the toggle component and acts accordingly to feedback on what to do
//eg if something changes the toggle to true, this will tell X to change colour or X to write text.
public class ReceiveToggleSignal : MonoBehaviour
{

    public bool changeColour;
    public Color colorOnState;
    public Color colorOffState;


    private Toggle toggle;
    private Renderer GORenderer;


    void Start()
    {
        //get the toggle component and read from it
        toggle = GetComponent<Toggle>();
        GORenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //first if the changeColor bool is true or not
        if (changeColour)
        {
            //check if the toggle is active if yes change the colour of this GameObject.
            if (toggle.active)
            {
                GORenderer.material.color = colorOnState;
            } else
            {
                GORenderer.material.color = colorOffState;
            }
        }

    }
}
