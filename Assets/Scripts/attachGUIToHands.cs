using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class attachGUIToHands : MonoBehaviour
{
    //Attach GUI objects to these
    [Header("Attach GUI Here")]

    public GameObject LeftHandGUI;
    public GameObject RightHandGUI;

    [Header("Button to reveal GUI, leave blank for always on")]

    //button that will show the GUI for each hand
    public InputActionReference LeftHandActivate = null;
    public InputActionReference RightHandActivate = null;

    private bool LeftGUIActive;
    private bool RightGUIActive;

    //adjusts y position of all the UI elements. 1.4f is on top of controller.
    private float yPositionOffset = 1.5f;


    //variables for both controllers positions and rotation.
    //Have rotations of the GUI look at the headset of the camera.
    [Header("Add Positions & rotations for headset and controllers")]


    public InputActionReference HeadsetPosition = null;
    Vector3 headsetPositionXYZ;


    public InputActionReference leftHandControllerPosition = null;
    public InputActionReference leftHandControllerRotation = null;

    Vector3 leftPositionXYZ;
    Quaternion leftRotation;

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

        leftHandControllerRotation.action.performed += getLeftControllerRotation;
        rightHandControllerRotation.action.performed += getRightControllerRotation;

        LeftHandActivate.action.performed += LeftHandGripped;
        LeftHandActivate.action.canceled += LeftHandReleased;

        RightHandActivate.action.performed += RightHandGripped;
        RightHandActivate.action.canceled += RightHandReleased;
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftHandActivate == null)
        {
            //position on top of characters hand and facing the headset postion
            LeftHandGUI.transform.position = new Vector3(leftPositionXYZ.x, leftPositionXYZ.y + yPositionOffset, leftPositionXYZ.z) + transform.position;
            Vector3 relativePosition = headsetPositionXYZ - LeftHandGUI.transform.position;
            LeftHandGUI.transform.rotation = Quaternion.LookRotation(relativePosition);
        } else
        {
            if (LeftGUIActive)
            {
                LeftHandGUI.transform.position = new Vector3(leftPositionXYZ.x, leftPositionXYZ.y + yPositionOffset, leftPositionXYZ.z) + transform.position;
                Vector3 relativePosition = headsetPositionXYZ - LeftHandGUI.transform.position;
                LeftHandGUI.transform.rotation = Quaternion.LookRotation(relativePosition);
            } else
            {
                //send this to some random place off the map
                LeftHandGUI.transform.position = new Vector3(0, 0, 0);
            }
        }


        if (RightHandActivate == null)
        {
            RightHandGUI.transform.position = new Vector3(rightPositionXYZ.x, rightPositionXYZ.y + yPositionOffset, rightPositionXYZ.z) + transform.position;
            Vector3 relativePosition = headsetPositionXYZ - RightHandGUI.transform.position;
            RightHandGUI.transform.rotation = Quaternion.LookRotation(relativePosition);
        }
        else
        {
            if (RightGUIActive)
            {
                RightHandGUI.transform.position = new Vector3(rightPositionXYZ.x, rightPositionXYZ.y + yPositionOffset, rightPositionXYZ.z) + transform.position;
                Vector3 relativePosition = headsetPositionXYZ - RightHandGUI.transform.position;
                RightHandGUI.transform.rotation = Quaternion.LookRotation(relativePosition);
            }
            else
            {
                //send this to some random place off the map
                RightHandGUI.transform.position = new Vector3(0, 0, 0);
            }
        }
    }

    private void onDestroy()
    {
        HeadsetPosition.action.performed -= getHeadsetPosition;

        leftHandControllerPosition.action.performed -= getLeftControllerPosition;
        rightHandControllerPosition.action.performed -= getRightControllerPosition;

        leftHandControllerRotation.action.performed -= getLeftControllerRotation;
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
    }

    private void getRightControllerPosition(InputAction.CallbackContext context)
    {
        rightPositionXYZ = context.ReadValue<Vector3>();
    }

    private void getLeftControllerRotation(InputAction.CallbackContext context)
    {
        leftRotation = context.ReadValue<Quaternion>();
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
