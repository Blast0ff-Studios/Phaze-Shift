// GENERATED AUTOMATICALLY FROM 'Assets/inputs/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""951f75f6-2805-41dc-a454-84408a259aa1"",
            ""actions"": [
                {
                    ""name"": ""Xaxis"",
                    ""type"": ""Value"",
                    ""id"": ""e0cc9a05-0b3b-40be-bdee-e3fe724f5bb5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Yaxis"",
                    ""type"": ""Value"",
                    ""id"": ""30773f07-6bcc-4cd9-8ed6-4d2596123fb0"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""73c1939b-2ae5-4d23-ad15-815add78b190"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""crouch"",
                    ""type"": ""Button"",
                    ""id"": ""87294b52-7d6a-4d73-9c2e-9c1dc1633388"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""mousePos"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2ab42559-82c3-43f1-9527-c14684a89b55"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""leftClick"",
                    ""type"": ""Button"",
                    ""id"": ""e7830c01-bb38-4ad7-8c4f-ee516e62fa1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""de522a15-c492-4268-91be-ef0db27f45bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""E"",
                    ""type"": ""Button"",
                    ""id"": ""ad241f04-d1d7-4104-bd1b-30cc9e3149f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""2a705851-328e-4f92-b849-1368ef4448d8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Xaxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1ee249d2-6982-4d55-be8f-8cbfe951f0e2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""Xaxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""25e228be-be7c-4b68-b433-0ac8dc1bad98"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""Xaxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""29bab975-1670-43bd-b855-b9bb69d1d3b2"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controler"",
                    ""action"": ""Xaxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""67af996e-4244-4b94-b6da-e7608679ca22"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""01a4a017-c321-42b4-920c-589c86ee5e5e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""Yaxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d5c7e630-0260-4697-adfb-f9f2309ac10b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""Yaxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1f8ac9ec-5ed3-4300-8969-15d0c7708c46"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controler"",
                    ""action"": ""Yaxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a01e86d-de59-4296-b7b7-af8a50073f31"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""801e57d5-474a-41c3-be8f-55637ebf3ae9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controler"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25be01b2-7cbe-4eed-a4c6-fd22ff691d50"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""baeaf8c4-5fb3-424e-9f08-85aebaba40ff"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""controler"",
                    ""action"": ""crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8b038e4-35d5-4745-b74e-cf7eecd4ffee"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""mousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24dc7ead-6683-40f1-9766-07e6edd8a05f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=4,y=5)"",
                    ""groups"": ""controler"",
                    ""action"": ""mousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c1d186a-64ef-48c7-b46c-f1c6ab52b9d3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""leftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""475bb240-d35a-4383-80b0-de45998a71e7"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controler"",
                    ""action"": ""leftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abe3733e-31a0-49b0-994d-b1b7ae27f6eb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47cdab93-a2c4-44c1-9221-9998b8876985"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""controler"",
            ""bindingGroup"": ""controler"",
            ""devices"": [
                {
                    ""devicePath"": ""<SwitchProControllerHID>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""keyboard"",
            ""bindingGroup"": ""keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Xaxis = m_Player.FindAction("Xaxis", throwIfNotFound: true);
        m_Player_Yaxis = m_Player.FindAction("Yaxis", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_crouch = m_Player.FindAction("crouch", throwIfNotFound: true);
        m_Player_mousePos = m_Player.FindAction("mousePos", throwIfNotFound: true);
        m_Player_leftClick = m_Player.FindAction("leftClick", throwIfNotFound: true);
        m_Player_ESC = m_Player.FindAction("ESC", throwIfNotFound: true);
        m_Player_E = m_Player.FindAction("E", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Xaxis;
    private readonly InputAction m_Player_Yaxis;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_crouch;
    private readonly InputAction m_Player_mousePos;
    private readonly InputAction m_Player_leftClick;
    private readonly InputAction m_Player_ESC;
    private readonly InputAction m_Player_E;
    public struct PlayerActions
    {
        private @Inputs m_Wrapper;
        public PlayerActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Xaxis => m_Wrapper.m_Player_Xaxis;
        public InputAction @Yaxis => m_Wrapper.m_Player_Yaxis;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @crouch => m_Wrapper.m_Player_crouch;
        public InputAction @mousePos => m_Wrapper.m_Player_mousePos;
        public InputAction @leftClick => m_Wrapper.m_Player_leftClick;
        public InputAction @ESC => m_Wrapper.m_Player_ESC;
        public InputAction @E => m_Wrapper.m_Player_E;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Xaxis.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXaxis;
                @Xaxis.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXaxis;
                @Xaxis.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnXaxis;
                @Yaxis.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYaxis;
                @Yaxis.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYaxis;
                @Yaxis.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnYaxis;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @crouch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @crouch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @crouch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @mousePos.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @mousePos.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @mousePos.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @leftClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @leftClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @leftClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @ESC.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @E.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnE;
                @E.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnE;
                @E.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnE;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Xaxis.started += instance.OnXaxis;
                @Xaxis.performed += instance.OnXaxis;
                @Xaxis.canceled += instance.OnXaxis;
                @Yaxis.started += instance.OnYaxis;
                @Yaxis.performed += instance.OnYaxis;
                @Yaxis.canceled += instance.OnYaxis;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @crouch.started += instance.OnCrouch;
                @crouch.performed += instance.OnCrouch;
                @crouch.canceled += instance.OnCrouch;
                @mousePos.started += instance.OnMousePos;
                @mousePos.performed += instance.OnMousePos;
                @mousePos.canceled += instance.OnMousePos;
                @leftClick.started += instance.OnLeftClick;
                @leftClick.performed += instance.OnLeftClick;
                @leftClick.canceled += instance.OnLeftClick;
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
                @E.started += instance.OnE;
                @E.performed += instance.OnE;
                @E.canceled += instance.OnE;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_controlerSchemeIndex = -1;
    public InputControlScheme controlerScheme
    {
        get
        {
            if (m_controlerSchemeIndex == -1) m_controlerSchemeIndex = asset.FindControlSchemeIndex("controler");
            return asset.controlSchemes[m_controlerSchemeIndex];
        }
    }
    private int m_keyboardSchemeIndex = -1;
    public InputControlScheme keyboardScheme
    {
        get
        {
            if (m_keyboardSchemeIndex == -1) m_keyboardSchemeIndex = asset.FindControlSchemeIndex("keyboard");
            return asset.controlSchemes[m_keyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnXaxis(InputAction.CallbackContext context);
        void OnYaxis(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnESC(InputAction.CallbackContext context);
        void OnE(InputAction.CallbackContext context);
    }
}
