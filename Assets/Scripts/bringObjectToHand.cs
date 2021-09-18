using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class bringObjectToHand : MonoBehaviour
{

    public GameObject LeftHandGUI;
    public GameObject RightHandGUI;

    //button that will show the GUI for each hand
    public InputActionReference LeftHandActivate = null;
    public InputActionReference RightHandActivate = null;

    private bool LeftGUIActive;
    private bool RightGUIActive;



    //variables for both controllers positions and rotation.
    //Have rotations of the GUI look at the headset of the camera.
    public InputActionReference HeadsetPosition = null;
    Vector3 headsetPositionXYZ;


    public InputActionReference leftHandControllerPosition = null;
    Vector3 leftPositionXYZ;

    public InputActionReference rightHandControllerPosition = null;
    public InputActionReference rightHandControllerRotation = null;

    Vector3 rightPositionXYZ;
    Quaternion rightRotation;

    // Start is called before the first frame update
    void Start()
    {
        HeadsetPosition.action.performed += getHeadsetPosition;


        leftHandControllerPosition.action.performed += getLeftControllerPosition;
        rightHandControllerPosition.action.performed += getRightControllerPosition;

        rightHandControllerRotation.action.performed += getRightControllerRotation;

        LeftHandActivate.action.performed += LeftHandGripped;
        LeftHandActivate.action.canceled += LeftHandReleased;

        RightHandActivate.action.performed += RightHandGripped;
        RightHandActivate.action.canceled += RightHandReleased;
    }

    // Update is called once per frame
    void Update()
    {


        //If the gui is active bring it to the top of the controller position
        //If not active move it to no mans land
        if (LeftGUIActive)
        {
            LeftHandGUI.transform.position = new Vector3(leftPositionXYZ.x, leftPositionXYZ.y + 0.2f, leftPositionXYZ.z) + transform.position;
        } else
        {
            LeftHandGUI.transform.position = new Vector3(0, 0, 0);
        }

        if (RightGUIActive)
        {
            RightHandGUI.transform.position = new Vector3(rightPositionXYZ.x, rightPositionXYZ.y + 0.2f, rightPositionXYZ.z) + transform.position;
            //change just the rotation for the x and z axis so it's always facing the user.
            Vector3 relativePosition = headsetPositionXYZ - RightHandGUI.transform.position;
            RightHandGUI.transform.rotation = Quaternion.LookRotation(relativePosition) ;
        }
        else
        {
            RightHandGUI.transform.position = new Vector3(0, 0, 0);
        }

    }

    //Remove the listener onDestroy

    private void onDestroy()
    {
        HeadsetPosition.action.performed -= getHeadsetPosition;

        leftHandControllerPosition.action.performed -= getLeftControllerPosition;
        rightHandControllerPosition.action.performed -= getRightControllerPosition;

        rightHandControllerRotation.action.performed -= getRightControllerRotation;

        LeftHandActivate.action.performed -= LeftHandGripped;
        LeftHandActivate.action.canceled -= LeftHandReleased;

        RightHandActivate.action.performed -= RightHandGripped;
        RightHandActivate.action.canceled -= RightHandReleased;

    }

    private void getHeadsetPosition(InputAction.CallbackContext context)
    {
        headsetPositionXYZ = context.ReadValue<Vector3>();
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

    private void getRightControllerRotation(InputAction.CallbackContext context)
    {
        rightRotation = context.ReadValue<Quaternion>();
    }

    private void LeftHandGripped(InputAction.CallbackContext context)
    {
        LeftGUIActive = true;
    }

    private void LeftHandReleased(InputAction.CallbackContext context)
    {
        LeftGUIActive = false;
    }

    private void RightHandGripped(InputAction.CallbackContext context)
    {
        RightGUIActive = true;
    }

    private void RightHandReleased(InputAction.CallbackContext context)
    {
        RightGUIActive = false;
    }
}
