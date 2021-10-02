using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//This script changes the toggle based on the movement of controller instead of tied to a game object.
public class changeToggleSignalMovement : MonoBehaviour
{

    public InputActionReference leftHandControllerPosition = null;
    Vector3 leftPositionXYZ;

    public InputActionReference rightHandControllerPosition = null;
    Vector3 rightPositionXYZ;

    public InputActionReference headsetPosition = null;
    Vector3 headPositionXYZ;


    // Start is called before the first frame update
    void Start()
    {
        leftHandControllerPosition.action.performed += getLeftControllerPosition;
        rightHandControllerPosition.action.performed += getRightControllerPosition;
        headsetPosition.action.performed += getHeadsetPosition;

    }

    // Update is called once per frame
    void Update()
    {
        //get the y distance of headset to controllers
        float rightHeadDistance = headPositionXYZ.y - rightPositionXYZ.y;
        float leftHeadDistance = headPositionXYZ.y - leftPositionXYZ.y;


        //currently based on distance between the hands and the headset
        if (leftHeadDistance < 0.5 && rightHeadDistance < 0.5)
        {
            GetComponent<Toggle>().ToggleOn();
        }
        else
        {
            GetComponent<Toggle>().ToggleOff();
        }
    }


    private void onDestroy()
    {
        //Remove the listener onDestroy
        leftHandControllerPosition.action.performed -= getLeftControllerPosition;
        rightHandControllerPosition.action.performed -= getRightControllerPosition;
        headsetPosition.action.performed -= getHeadsetPosition;
    }

    private void getLeftControllerPosition(InputAction.CallbackContext context)
    {
        leftPositionXYZ = context.ReadValue<Vector3>();
        print("Left POS: " + leftPositionXYZ);
    }

    private void getRightControllerPosition(InputAction.CallbackContext context)
    {
        rightPositionXYZ = context.ReadValue<Vector3>();
        print("Right POS: " + rightPositionXYZ);
    }

    private void getHeadsetPosition(InputAction.CallbackContext context)
    {
        headPositionXYZ = context.ReadValue<Vector3>();
        print("Head POS: " + headPositionXYZ);
    }
}
