using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//This script changes the toggle based on the movement of controller instead of tied to a game object.
public class changeToggleSignalMovement : MonoBehaviour
{

    //add the XR Default Input Action to this
    public InputActionAsset actionAsset;

    //using an actionmap to reduce the number of references on this page
    private InputActionMap rightControllerMap;
    private InputActionMap leftControllerMap;
    private InputActionMap HMDMap;

    //Input actions for position and rotation
    private InputAction getRightPosition;
    private InputAction getLeftPosition;
    private InputAction getHMDPosition;

    Vector3 leftPositionXYZ;
    Vector3 rightPositionXYZ;
    Vector3 headPositionXYZ;


    // Start is called before the first frame update
    void Start()
    {
        //Find the action map so that we can reference each of the references inside
        //this one is for right controller only.
        rightControllerMap = actionAsset.FindActionMap("XRI RightHand");
        rightControllerMap.Enable();

        leftControllerMap = actionAsset.FindActionMap("XRI LeftHand");
        leftControllerMap.Enable();

        HMDMap = actionAsset.FindActionMap("XRI HMD");
        HMDMap.Enable();

        //POSITION
        getRightPosition = rightControllerMap.FindAction("Position");
        getLeftPosition = leftControllerMap.FindAction("Position");
        getHMDPosition = HMDMap.FindAction("Position");

        getRightPosition.performed += context => getRightControllerPosition(context);
        getLeftPosition.performed += context => getLeftControllerPosition(context);
        getHMDPosition.performed += context => getHeadsetPosition(context);
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
            GetComponent<ToggleComponent>().ToggleOn();
        }
        else
        {
            GetComponent<ToggleComponent>().ToggleOff();
        }
    }


    private void onDestroy()
    {
        getRightPosition.performed -= context => getRightControllerPosition(context);
        getLeftPosition.performed -= context => getLeftControllerPosition(context);
        getHMDPosition.performed -= context => getHeadsetPosition(context);
    }

    private void getLeftControllerPosition(InputAction.CallbackContext context)
    {
        leftPositionXYZ = context.ReadValue<Vector3>();
        //print("Left POS: " + leftPositionXYZ);
    }

    private void getRightControllerPosition(InputAction.CallbackContext context)
    {
        rightPositionXYZ = context.ReadValue<Vector3>();
        //print("Right POS: " + rightPositionXYZ);
    }

    private void getHeadsetPosition(InputAction.CallbackContext context)
    {
        headPositionXYZ = context.ReadValue<Vector3>();
        //print("Head POS: " + headPositionXYZ);
    }
}
