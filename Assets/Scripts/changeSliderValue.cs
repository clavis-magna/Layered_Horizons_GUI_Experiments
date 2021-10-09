using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public enum sliderType
{
    Button,
    ClickDrag,
    Experimental
}

public enum handSelect
{
    Left,
    Right
}

public class changeSliderValue : MonoBehaviour
{
    public sliderType currentSliderType;

    public InputActionAsset actionAsset;

    TextMeshProUGUI textDisplay;

    //using an actionmap to reduce the number of references on this page
    private InputActionMap rightControllerMap;
    private InputActionMap leftControllerMap;




    [Header("Current Slider Type: Button")]
    //assign buttons to increase and decrease the slider.
    public InputActionReference IncreaseSliderInputRef;
    public InputActionReference DecreaseSliderInputRef;

    //Button
    //Input actions for increasing and decreasing slider value through button
    private InputAction buttonIncreaseValue;
    private InputAction buttonDecreaseValue;

    private bool IncreaseActionDetect = false;
    private bool DecreaseActionDetect = false;




    [Header("Current Slider Type: ClickDrag")]
    public handSelect activeHand;
    public InputActionReference activateDragInputRef;

    private InputAction getRightPosition;
    private InputAction getLeftPosition;

    private Vector3 controllerPositionXYZ;

    //Vector3 startControllerPosition;
    //Vector3 endControllerPosition;

    private float startControllerPosition;
    private float endControllerPosition;

    private float sliderValue;
    private float displayedValue;
    private float movementAmount;

    private bool clickDragActive;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = GetComponentInChildren<TextMeshProUGUI>();


        //Find the action map so that we can reference each of the references inside
        //this one is for right controller only.
        rightControllerMap = actionAsset.FindActionMap("XRI RightHand");
        rightControllerMap.Enable();

        leftControllerMap = actionAsset.FindActionMap("XRI LeftHand");
        leftControllerMap.Enable();

        switch (currentSliderType)
        {
            case sliderType.Button:
                textDisplay.text = "the button to increase decrease the slider is x and y";

                IncreaseSliderInputRef.action.performed += IncreaseAction;
                DecreaseSliderInputRef.action.performed += DecreaseAction;

                break;
            case sliderType.ClickDrag:
                textDisplay.text = "Click and drag to change the slider";

                switch (activeHand)
                {
                    case handSelect.Right:
                        rightControllerMap = actionAsset.FindActionMap("XRI RightHand");
                        rightControllerMap.Enable();
                        getRightPosition = rightControllerMap.FindAction("Position");
                        getRightPosition.performed += context => getControllerPosition(context);
                        break;

                    case handSelect.Left:
                        leftControllerMap = actionAsset.FindActionMap("XRI LeftHand");
                        leftControllerMap.Enable();
                        getLeftPosition = leftControllerMap.FindAction("Position");
                        getLeftPosition.performed += context => getControllerPosition(context);
                        break;
                }
                activateDragInputRef.action.performed += activateChangeValue;
                activateDragInputRef.action.canceled += deactivateChangeValue;

                break;
        }




    }

    // Update is called once per frame
    void Update()
    {
        switch (currentSliderType)
        {
            case sliderType.Button:
                //very basic if the slider value increase button is pushed increase vice versa for decrease
                if (IncreaseActionDetect == true)
                {
                    IncreaseActionDetect = false;
                    GetComponent<SliderComponent>().IncreaseValue();
                }
                if (DecreaseActionDetect == true)
                {
                    DecreaseActionDetect = false;
                    GetComponent<SliderComponent>().DecreaseValue();
                }
                break;
            case sliderType.ClickDrag:


                if (clickDragActive)
                {
                    endControllerPosition = controllerPositionXYZ.y;

                    //get the distance that controller moved from press to release.
                    movementAmount = startControllerPosition - endControllerPosition;

                    //this value gets placed on the text
                    displayedValue = sliderValue - movementAmount;
                }

                //text is indicated by what is on displayedValue
                textDisplay.text = "val" + displayedValue;
                break;
        }
    }

    private void onDestroy()
    {
        IncreaseSliderInputRef.action.performed -= IncreaseAction;
        DecreaseSliderInputRef.action.performed -= DecreaseAction;

    }

    //these two are for the button
    private void IncreaseAction(InputAction.CallbackContext context)
    {
        IncreaseActionDetect = true;
    }

    private void DecreaseAction(InputAction.CallbackContext context)
    {
        DecreaseActionDetect = true;
    }



    //this is for the click drag
    private void activateChangeValue(InputAction.CallbackContext context)
    {
        clickDragActive = true;

        //slider value is always what you receive back from the slider Component updated always.
        sliderValue = GetComponent<SliderComponent>().value;
        print("SliderBV: " + sliderValue);
        startControllerPosition = controllerPositionXYZ.y;

        ////at press make the displayed value = slider

        ////save the position of the controller when first pressed the activate button
        //startControllerPosition = controllerPositionXYZ;

    }

    private void deactivateChangeValue(InputAction.CallbackContext context)
    {
        clickDragActive = false;



        ////save the position of the controller when activate button is released
        //endControllerPosition = controllerPositionXYZ;





        //////define the slider by the movement when the controller position was moved.
        ////float newSliderValue = Mathf.Abs(startControllerPosition.y - endControllerPosition.y);
        GetComponent<SliderComponent>().DefineValue(displayedValue);

    }

    private void getControllerPosition(InputAction.CallbackContext context)
    {
        controllerPositionXYZ = context.ReadValue<Vector3>();
        //print("Controller: " + controllerPositionXYZ);
    }
}
