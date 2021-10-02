using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class changeToggleSignal : MonoBehaviour
{
    [Header("Button Req.")]

    public bool pressReactive;
    public InputActionReference buttonInputReference = null;
    private bool pressActivated = false;


    [Header("Touch Req.")]
    //turning off touch reactive means the object doesn't have to collide first with the controller to be activated
    public bool touchReactive;
    private bool touchActivated = false;

    private float buttonHitAgainTime = 0.5f;
    private float canHitAgain;


    // Start is called before the first frame update
    void Start()
    {
        if (pressReactive)
        {
            buttonInputReference.action.performed += buttonPressed;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //check what configuration to use in regards to the touch and press reactive variables.
        if (touchReactive && !pressReactive)
        {
            //Only touch the collider of this GO to change toggle
            if (touchActivated)
            {
                touchActivated = false;
                GetComponent<Toggle>().ToggleAlternate();
            }
        } else if (!touchReactive && pressReactive)
        {
            //Only press button to change toggle
            if (pressActivated)
            {
                pressActivated = false;
                GetComponent<Toggle>().ToggleAlternate();
            }
        }
        else if (touchReactive && pressReactive)
        {
            //Touch the collider of this GO AND press button to change toggle
            if (pressActivated && touchActivated)
            {
                pressActivated = false;
                touchActivated = false;

                GetComponent<Toggle>().ToggleAlternate();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //On trigger check if this can be hit. There is a delay applied to make sure that can't be hit constantly
        if (other.CompareTag("GameController") && canHitAgain < Time.time)
        {
            canHitAgain = Time.time + buttonHitAgainTime;
            touchActivated = true;
        }

    }

    private void onDestroy()
    {
        if (pressReactive)
        {
            buttonInputReference.action.performed -= buttonPressed;
        }
    }

    private void buttonPressed(InputAction.CallbackContext context)
    {
        pressActivated = true;
    }

}
