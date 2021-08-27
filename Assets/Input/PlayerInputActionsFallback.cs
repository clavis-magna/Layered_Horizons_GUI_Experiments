// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInputActionsFallback.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActionsFallback : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActionsFallback()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActionsFallback"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""1f8ad19f-31b0-415c-9916-1ba29ef97c7f"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""9d1def7f-1ff0-41f1-8e6d-b2a9ea7c9057"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""571b89bd-74bf-4672-8ee7-6f88ebc91060"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3b4c065a-4675-4419-bd57-2cd5ca680616"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3165371-7f16-4f28-9670-4fff22ac1174"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Rotate = m_Movement.FindAction("Rotate", throwIfNotFound: true);
        m_Movement_Forward = m_Movement.FindAction("Forward", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Rotate;
    private readonly InputAction m_Movement_Forward;
    public struct MovementActions
    {
        private @PlayerInputActionsFallback m_Wrapper;
        public MovementActions(@PlayerInputActionsFallback wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Movement_Rotate;
        public InputAction @Forward => m_Wrapper.m_Movement_Forward;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotate;
                @Forward.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnForward;
                @Forward.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnForward;
                @Forward.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnForward;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Forward.started += instance.OnForward;
                @Forward.performed += instance.OnForward;
                @Forward.canceled += instance.OnForward;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface IMovementActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnForward(InputAction.CallbackContext context);
    }
}
