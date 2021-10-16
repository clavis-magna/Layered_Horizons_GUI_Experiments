using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;



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

    //get the XRController to apply the haptics
    //settings for amp and duration here too
    private InputDevice leftHand;
    private InputDevice rightHand;

    private bool haptic;

    private float _amplitude = 1.0f;
    private float _duration = 0.1f;


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


        //added this example of haptics but still needs to be done so that it only occurs once when the movement modes change.
        if (leftHeadDistance == 0.5 || rightHeadDistance == 0.5)
        {
            Rumble(rightHand);
            Rumble(leftHand);
        }


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

    //script pulled from here
    //https://forum.unity.com/threads/unity-support-for-openxr-in-preview.1023613/page-5#post-7046953
    /// <summary>
    /// Send a rumble command to a device
    /// </summary>
    /// <param name="device">Device to send rumble to</param>
    private void Rumble(InputDevice device)
    {
        // Setting channel to 1 will work in 1.1.1 but will be fixed in future versions such that 0 would be the correct channel.
        var channel = 1;
        var command = UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand.Create(channel, _amplitude, _duration);
        device.ExecuteCommand(ref command);
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
        leftHand = context.control.device;

    }

    private void getRightControllerPosition(InputAction.CallbackContext context)
    {
        rightPositionXYZ = context.ReadValue<Vector3>();
        //print("Right POS: " + rightPositionXYZ);

        //assign the controller to send haptics
        rightHand = context.control.device;
        //// If the action that was performed was on a XRController device then rumble
        //if (context.control.device is XRController device)
        //    Rumble(device);
    }

    private void getHeadsetPosition(InputAction.CallbackContext context)
    {
        headPositionXYZ = context.ReadValue<Vector3>();
        //print("Head POS: " + headPositionXYZ);
    }
}
