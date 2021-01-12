// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/GameSystem/InputActions/PlayerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""6e86d594-3d20-40d3-9de8-e5d03b840251"",
            ""actions"": [
                {
                    ""name"": ""Movements"",
                    ""type"": ""Value"",
                    ""id"": ""2e9041da-d872-4187-b98e-37c2e9db3afd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireGun"",
                    ""type"": ""Button"",
                    ""id"": ""33a2caff-0d0e-486b-893a-be661a3e920f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""dbc2a524-d7e8-4022-a777-e1561d701b04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Focus"",
                    ""type"": ""Button"",
                    ""id"": ""f801db38-df44-417b-b257-b07132be6e80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ultimate"",
                    ""type"": ""Button"",
                    ""id"": ""47959966-bf4f-4f78-8497-6e2ffb2cad61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireMissiles"",
                    ""type"": ""Button"",
                    ""id"": ""5bb5aca9-4a69-446b-981c-36a74274ff7d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""1d1ec3aa-ee7c-4ea4-a7ca-572da655efdc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2172ad21-cd03-4fec-a755-0b451bc1915e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movements"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d4c112c1-cbba-4355-8203-078f27b48281"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movements"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9b903539-5713-4eef-9065-b820448660f8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movements"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d237d260-4297-4c94-96fc-aa6605ac3c2e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movements"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""51191e58-c760-4a2a-93ee-0aaa03d1669c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movements"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2daee8a9-e2a4-4920-908b-27796b3ca8f0"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""FireGun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7f0d3a0-970a-423e-b8d3-b40634596763"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a71164f-9036-4368-bbda-cf1e24ae8d9b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Focus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7261d3ba-40ca-498d-8c09-df50aefe7eff"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Ultimate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1d2a2e8-cde2-4d64-980a-df263d734423"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""FireMissiles"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fc29dba-a63c-4fc5-bcaf-e33d6f988344"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""292ccac1-c7d3-47a0-a701-87142ad15cc7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": []
        }
    ]
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Movements = m_Keyboard.FindAction("Movements", throwIfNotFound: true);
        m_Keyboard_FireGun = m_Keyboard.FindAction("FireGun", throwIfNotFound: true);
        m_Keyboard_Accept = m_Keyboard.FindAction("Accept", throwIfNotFound: true);
        m_Keyboard_Focus = m_Keyboard.FindAction("Focus", throwIfNotFound: true);
        m_Keyboard_Ultimate = m_Keyboard.FindAction("Ultimate", throwIfNotFound: true);
        m_Keyboard_FireMissiles = m_Keyboard.FindAction("FireMissiles", throwIfNotFound: true);
        m_Keyboard_Pause = m_Keyboard.FindAction("Pause", throwIfNotFound: true);
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

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Movements;
    private readonly InputAction m_Keyboard_FireGun;
    private readonly InputAction m_Keyboard_Accept;
    private readonly InputAction m_Keyboard_Focus;
    private readonly InputAction m_Keyboard_Ultimate;
    private readonly InputAction m_Keyboard_FireMissiles;
    private readonly InputAction m_Keyboard_Pause;
    public struct KeyboardActions
    {
        private @PlayerInputAction m_Wrapper;
        public KeyboardActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movements => m_Wrapper.m_Keyboard_Movements;
        public InputAction @FireGun => m_Wrapper.m_Keyboard_FireGun;
        public InputAction @Accept => m_Wrapper.m_Keyboard_Accept;
        public InputAction @Focus => m_Wrapper.m_Keyboard_Focus;
        public InputAction @Ultimate => m_Wrapper.m_Keyboard_Ultimate;
        public InputAction @FireMissiles => m_Wrapper.m_Keyboard_FireMissiles;
        public InputAction @Pause => m_Wrapper.m_Keyboard_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Movements.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovements;
                @Movements.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovements;
                @Movements.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovements;
                @FireGun.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFireGun;
                @FireGun.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFireGun;
                @FireGun.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFireGun;
                @Accept.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnAccept;
                @Focus.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFocus;
                @Focus.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFocus;
                @Focus.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFocus;
                @Ultimate.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnUltimate;
                @Ultimate.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnUltimate;
                @Ultimate.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnUltimate;
                @FireMissiles.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFireMissiles;
                @FireMissiles.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFireMissiles;
                @FireMissiles.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnFireMissiles;
                @Pause.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movements.started += instance.OnMovements;
                @Movements.performed += instance.OnMovements;
                @Movements.canceled += instance.OnMovements;
                @FireGun.started += instance.OnFireGun;
                @FireGun.performed += instance.OnFireGun;
                @FireGun.canceled += instance.OnFireGun;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
                @Focus.started += instance.OnFocus;
                @Focus.performed += instance.OnFocus;
                @Focus.canceled += instance.OnFocus;
                @Ultimate.started += instance.OnUltimate;
                @Ultimate.performed += instance.OnUltimate;
                @Ultimate.canceled += instance.OnUltimate;
                @FireMissiles.started += instance.OnFireMissiles;
                @FireMissiles.performed += instance.OnFireMissiles;
                @FireMissiles.canceled += instance.OnFireMissiles;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface IKeyboardActions
    {
        void OnMovements(InputAction.CallbackContext context);
        void OnFireGun(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
        void OnFocus(InputAction.CallbackContext context);
        void OnUltimate(InputAction.CallbackContext context);
        void OnFireMissiles(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
