using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.LowLevel;


public enum ControlMode
{
    KeyboardMouse,
    Gamepad,
    Touch
}

public class PlayerController : MonoBehaviour
{
    // General Events
    public UnityEvent OnModeChange;
    public UnityEvent OnAnyKey;

    private ControlMode _currentMode = ControlMode.KeyboardMouse;
    public ControlMode currentMode => _currentMode;

    // Action Events
    public UnityEvent OnFireStart;
    public UnityEvent OnFireEnd;

    private Vector2 _moveVector;
    private bool _fire;
    
    public Vector2 moveVector => _moveVector;
    public bool fire => _fire;

    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onEvent +=
        (eventPtr, device) =>
        {
            _HandleRawInputSystemEvent(eventPtr, device);
        };

    }

    // Update is called once per frame
    void Update()
    {
        _HandleKeyboardInput();
        _HandleGamepadInput();
    }

    void _HandleGamepadInput()
    {
        if (_currentMode != ControlMode.Gamepad) return;
        Gamepad gamepadDevice = Gamepad.current;

        _moveVector = new Vector2(0, 0);

        Vector2 leftStick = gamepadDevice.leftStick.ReadValue();
        _moveVector.x = leftStick.x;
        _moveVector.y = leftStick.y;

        if (gamepadDevice.dpad.up.isPressed)
        {
            _moveVector.y = 1;
        }
        else if (gamepadDevice.dpad.down.isPressed)
        {
            _moveVector.y = -1;
        }

        if (gamepadDevice.dpad.left.isPressed)
        {
            _moveVector.x = -1;
        }
        else if (gamepadDevice.dpad.right.isPressed)
        {
            _moveVector.x = 1;
        }

        if (gamepadDevice.leftTrigger.ReadValue() > 0.1f)
        {
            gamepadDevice.SetMotorSpeeds(gamepadDevice.leftTrigger.ReadValue() * 0.5f,0.0f);
        }
        else
        {
            gamepadDevice.SetMotorSpeeds(0,0);
        }
    }
    void _HandleKeyboardInput()
    {
        if (_currentMode != ControlMode.KeyboardMouse) return;
        Keyboard keyboardDevice = Keyboard.current;
        var wKey = keyboardDevice.wKey;
        var aKey = keyboardDevice.aKey;
        var sKey = keyboardDevice.sKey;
        var dKey = keyboardDevice.dKey;


        _moveVector = new Vector2(0,0);
        if (wKey.isPressed)
        {
            _moveVector.y = 1;
        }
        else if (sKey.isPressed)
        {
            _moveVector.y -= 1;
        }

        if (aKey.isPressed)
        {
            _moveVector.x = -1;
        }
        else if (dKey.isPressed)
        {
            _moveVector.x = 1;
        }

    }

    void _HandleRawInputSystemEvent(
        UnityEngine.InputSystem.LowLevel.InputEventPtr eventPointer,
        InputDevice eventDevice
        )
    {
        Keyboard KeyboardDevice = eventDevice as Keyboard;
        Mouse MouseDevice = eventDevice as Mouse;
        Gamepad GamepadDevice = eventDevice as Gamepad;
        Touchscreen TouchScreenDevice = eventDevice as Touchscreen;

        // Get Mode to change to
        ControlMode NewControlMode = 0;

        if (GamepadDevice != null)
        {
            NewControlMode = ControlMode.Gamepad;
        }
        else if (TouchScreenDevice != null)
        {
            NewControlMode = ControlMode.Touch;
        }
        // Default to Keyboard if no other support device is triggered
        else
        {
            NewControlMode = ControlMode.KeyboardMouse;

            if (KeyboardDevice != null)
            {
                if (KeyboardDevice.jKey.wasPressedThisFrame)
                {

                }
            }
        }

        // Actual mode change. Set and call event
        if (NewControlMode != _currentMode)
        {
            _currentMode = NewControlMode;
            OnModeChange.Invoke();
        }

        Debug.Log($"Got Input Event| Current Control Mode = {System.Enum.GetName(typeof(ControlMode),_currentMode)}");
    }
}
