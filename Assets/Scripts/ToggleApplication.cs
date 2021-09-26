using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ToggleApplication : MonoBehaviour
{
    public InputActionReference ActionRef = null;

    //On is the toggle
    //button hit is for collider contanct
    //buttonGripped is if the Input Reference has been pressed.
    private bool buttonGripped = false;
    private bool on = false;
    private bool buttonHit = false;

    private float buttonHitAgainTime = 0.5f;
    private float canHitAgain;

    // Start is called before the first frame update
    void Start()
    {
        ActionRef.action.started += DoPressedThing;
        ActionRef.action.performed += DoChangeThing;
        ActionRef.action.canceled += DoReleasedThing;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Toggle: " + on);
        if (buttonHit == true && buttonGripped == true)
        {
            buttonHit = false;

            //if on is true make false, if on is false make it true
            on = !on;
            

            //turn on off spotlight here
            if (on)
            {
                GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                GetComponent<Toggle>().active = true;
            }
            else
            {
                GetComponent<Renderer>().material.color = new Color(0, 1, 0);
                GetComponent<Toggle>().active = false;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("An Object has entered the trigger");
        //Delay time
        if (other.CompareTag("GameController") && canHitAgain < Time.time)
        {
            canHitAgain = Time.time + buttonHitAgainTime;
            buttonHit = true;
            //print("CONTROLLER: Detected collision between button and controller");
        }

    }


    private void onDestroy()
    {
        //Remove the listener onDestroy
        ActionRef.action.started -= DoPressedThing;
        ActionRef.action.performed -= DoChangeThing;
        ActionRef.action.canceled -= DoReleasedThing;

    }

    private void DoPressedThing(InputAction.CallbackContext context)
    {
        //print("Pressed");
    }

    private void DoChangeThing(InputAction.CallbackContext context)
    {
        buttonGripped = true;
    }

    private void DoReleasedThing(InputAction.CallbackContext context)
    {
        //print("Released");
        buttonGripped = false;

    }
}
