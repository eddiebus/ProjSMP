using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;


public enum ControlMode
{
    KeyboardMouse,
    Gamepad,
    Touch
}

public class ActionButton
{
    public ActionButton()
    {
        PressedThisFrame = new UnityEvent();
        ReleasedThisFrame = new UnityEvent();
        Value = 0.0f;
    }
    
    public float Value;
    public UnityEvent PressedThisFrame;
    public UnityEvent ReleasedThisFrame;
}

public class PlayerController : MonoBehaviour
{
    // General Events
    public UnityEvent OnModeChange;
    public UnityEvent OnAnyKey;

    private ControlMode _currentMode = ControlMode.KeyboardMouse;
    public ControlMode currentMode => _currentMode;

    private Vector2 _moveVector;
    private ActionButton _fire;
    
    public Vector2 moveVector => _moveVector;
    public ActionButton fire => _fire;

    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onEvent +=
        (eventPtr, device) =>
        {
            _HandleRawInputSystemEvent(eventPtr, device);
        };
        _fire = new ActionButton();
    }

    // Update is called once per frame
    void Update()
    {
        _HandleKeyboardInput();
        //_HandleGamepadInput();
    }

    void _HandleGamepadInput()
    {
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

    }
    void _HandleKeyboardInput()
    {
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

        _fire.Value = 0.0f;
        
        
        ButtonControl[] FireKeys = { keyboardDevice.jKey,keyboardDevice.spaceKey };

        foreach (var button in FireKeys)
        {
            if (button.wasPressedThisFrame)
            {
                _fire.ReleasedThisFrame.Invoke();
            }
            else if (button.wasPressedThisFrame)
            {
                _fire.PressedThisFrame.Invoke();
            }
            else if (button.isPressed)
            {
                _fire.Value = 1.0f;
            }
        }
    }

    void _HandleRawInputSystemEvent(
        UnityEngine.InputSystem.LowLevel.InputEventPtr eventPointer,
        InputDevice eventDevice
        )
    {

    }
}
