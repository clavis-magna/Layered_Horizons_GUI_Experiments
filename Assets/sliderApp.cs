using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class sliderApp : MonoBehaviour
{
    public InputActionReference IncreaseSliderInputRef = null;
    public InputActionReference DecreaseSliderInputRef = null;

    private bool IncreaseActionDetect = false;
    private bool DecreaseActionDetect = false;


    // Start is called before the first frame update
    void Start()
    {
        IncreaseSliderInputRef.action.performed += IncreaseAction;
        DecreaseSliderInputRef.action.performed += DecreaseAction;
    }

    // Update is called once per frame
    void Update()
    {

        //very basic if the slider value increase button is pushed increase vice versa for decrease
        if (IncreaseActionDetect == true)
        {
            IncreaseActionDetect = false;
            GetComponent<Slider>().IncreaseValue();
        }

        if (DecreaseActionDetect == true)
        {
            DecreaseActionDetect = false;
            GetComponent<Slider>().DecreaseValue();
        }

        float sliderVal = GetComponent<Slider>().value;

        GetComponent<Renderer>().material.color = new Color(sliderVal, 0, 0);


    }


    private void onDestroy()
    {
        IncreaseSliderInputRef.action.performed -= IncreaseAction;
        DecreaseSliderInputRef.action.performed -= DecreaseAction;

    }

    private void IncreaseAction(InputAction.CallbackContext context)
    {
        IncreaseActionDetect = true;
    }

    private void DecreaseAction(InputAction.CallbackContext context)
    {
        DecreaseActionDetect = true;
    }

}
