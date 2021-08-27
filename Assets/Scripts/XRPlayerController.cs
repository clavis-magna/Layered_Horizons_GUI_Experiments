using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR;

public class XRPlayerController : MonoBehaviour
{
    [Header("Reference to the controller object in XRRig heirachy")]
    public GameObject hand;
    public float normalMoveSpeed = 1;
    public InputActionReference testReference = null;
    [Header("True to allow desktop controlls")]
    // we want to make this automatic eventually
    public bool desktopControls = true;

    // values that will come from user input
    float speedMultiplier;
    float fallbackSpeedMultiplier;

    PlayerInputActionsFallback playerInputActionsFallback;
    Vector2 rotation = Vector2.zero;

    public float desktopControlsMovespeed = 0.1f;

    private void Awake()
    {
        playerInputActionsFallback = new PlayerInputActionsFallback();
        playerInputActionsFallback.Movement.Forward.performed += Forward;
        playerInputActionsFallback.Movement.Forward.canceled += Stop;
        playerInputActionsFallback.Movement.Rotate.performed += mouseLook;
    }

    private void Start()
    {
        testReference.action.started += DoPressedThing;
        testReference.action.performed += DoChangeThing;
        testReference.action.canceled += DoReleasedThing;
    }

    public void Update()
    {
        // XR movement
        // we will add a use configerable choice for this - one or the other

        // go in direction looking
        //transform.position += Camera.main.transform.forward * normalMoveSpeed * Time.deltaTime * speedMultiplier;

        // go in direction pointing
        transform.position += hand.transform.forward * normalMoveSpeed * Time.deltaTime * speedMultiplier;

        // Fallback movement
        if (desktopControls)
        {
            transform.position += Camera.main.transform.forward * normalMoveSpeed * Time.deltaTime * fallbackSpeedMultiplier;
        }
    }

    private void OnEnable()
    {
        testReference.asset.Enable();
        playerInputActionsFallback.Enable();
    }

    private void OnDisable()
    {
        testReference.asset.Disable();
        playerInputActionsFallback.Disable();
    }

    private void mouseLook(InputAction.CallbackContext context)
    {
        if (desktopControls)
        {
            Vector2 mouseIn = context.ReadValue<Vector2>();
            Camera.main.transform.parent.transform.Rotate(new Vector3(0f, mouseIn.x, 0f) * desktopControlsMovespeed, Space.World);
            Camera.main.transform.parent.transform.Rotate(new Vector3(0f, 0f, mouseIn.y) * desktopControlsMovespeed, Space.Self);
        }
    }

    private void OnDestroy()
    {
        testReference.action.started -= DoPressedThing;
        testReference.action.performed -= DoChangeThing;
        testReference.action.canceled -= DoReleasedThing;
    }

    private void Forward(InputAction.CallbackContext context)
    {
        print("Forward Pressed");
        fallbackSpeedMultiplier = 1;
    }

    private void Stop(InputAction.CallbackContext context)
    {
        print("Forward Released");
        fallbackSpeedMultiplier = 0;
    }

    private void DoPressedThing(InputAction.CallbackContext context)
    {
        print("Pressed");
    }

    private void DoChangeThing(InputAction.CallbackContext context)
    {
        speedMultiplier = context.ReadValue<float>(); 
        print(speedMultiplier);
    }

    private void DoReleasedThing(InputAction.CallbackContext context)
    {
        print("Released");
        speedMultiplier = 0;
    }
}
