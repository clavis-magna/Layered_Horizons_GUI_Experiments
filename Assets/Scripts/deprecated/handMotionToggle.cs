using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class handMotionToggle : MonoBehaviour
{
    //a light object to give visual feedback that the control has changed
    public Light spotLight;

    //variables for each of the input actions that need to be used.
    public InputActionReference leftHandControllerPosition = null;
    Vector3 leftPositionXYZ;

    public InputActionReference rightHandControllerPosition = null;
    Vector3 rightPositionXYZ;


    // Start is called before the first frame update
    void Start()
    {
        //This gets the listener to start checking for changes.

        leftHandControllerPosition.action.performed += getLeftControllerPosition;
        rightHandControllerPosition.action.performed += getRightControllerPosition;
        spotLight.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (leftPositionXYZ.y < 0.6 && rightPositionXYZ.y < 0.6)
        {
            spotLight.enabled = true;

            XRPlayerController.movementMode = false;
        } else
        {
            spotLight.enabled = false;

            XRPlayerController.movementMode = true;
        }
    }

    private void onDestroy()
    {
        //Remove the listener onDestroy
        leftHandControllerPosition.action.performed -= getLeftControllerPosition;
        rightHandControllerPosition.action.performed -= getRightControllerPosition;

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

}
