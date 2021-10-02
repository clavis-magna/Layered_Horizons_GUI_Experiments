using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SliderApplication : MonoBehaviour
{

    public InputActionReference ActionRef = null;
    public InputActionReference ControllerPosition = null;
    private Vector3 controllerPositionXYZ;

    private bool buttonGripped = false;
    private bool buttonHit = false;

    public float sliderValue;

    public GameObject bar1;
    public GameObject bar2;
    public GameObject bar3;
    public GameObject bar4;
    public GameObject bar5;


    // Start is called before the first frame update
    void Start()
    {
        ControllerPosition.action.performed += getControllerPosition;

        ActionRef.action.performed += DoChangeThing;
        ActionRef.action.canceled += DoReleasedThing;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SliderValue: " + sliderValue);
        if (buttonHit == true && buttonGripped == true)
        {
            sliderValue += 0.01f;
            buttonHit = false;
        }

        if (sliderValue > 0.2)
        {
            bar1.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
        if (sliderValue > 0.4)
        {
            bar2.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
        if (sliderValue > 0.6)
        {
            bar3.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
        if (sliderValue > 0.8)
        {
            bar4.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
        if (sliderValue > 1.0)
        {
            bar5.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
        //else
        //{
        //    //bar1.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        //    //bar2.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        //    //bar3.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        //    //bar4.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        //    //bar5.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        //}
    }

    private void onDestroy()
    {
        //Remove the listener onDestroy
        ControllerPosition.action.performed -= getControllerPosition;

        ActionRef.action.performed -= DoChangeThing;
        ActionRef.action.canceled -= DoReleasedThing;

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("An Object has entered the trigger");
        //Delay time
        if (other.CompareTag("GameController"))
        {
            buttonHit = true;
        }

    }

    private void getControllerPosition(InputAction.CallbackContext context)
    {
        //track the position of the controller that is activating.
        controllerPositionXYZ = context.ReadValue<Vector3>();
    }

    private void DoChangeThing(InputAction.CallbackContext context)
    {
        //controllerEndPos = controllerPositionXYZ;
        buttonGripped = true;
        //print("ControllerAction performed Called");

    }

    private void DoReleasedThing(InputAction.CallbackContext context)
    {
        //print("ControllerAction canceled Called");
        buttonGripped = false;
    }
}
