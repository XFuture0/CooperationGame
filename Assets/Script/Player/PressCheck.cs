using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressCheck : MonoBehaviour
{
    private PlayerInputControl inputControl;
    [Header("检测按键")]
    public bool IsPressW;
    public bool IsPressA;
    public bool IsPressS;
    public bool IsPressD;
    [Header("广播")]
    public FloatEventSO Bubble_Speed_Event;//确定泡泡的飞行方向
    [Header("事件监听")]
    public VoidEventSO CallBubbleFlyEvent;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
    }
    private void OnEnable()
    {
        inputControl.Enable();
        inputControl.PressCheck.W.performed += OnW;
        inputControl.PressCheck.A.performed += OnA;
        inputControl.PressCheck.S.performed += OnS;
        inputControl.PressCheck.D.performed += OnD;
        inputControl.PressCheck.W.canceled += CancelW;
        inputControl.PressCheck.A.canceled += CancelA;
        inputControl.PressCheck.S.canceled += CancelS;
        inputControl.PressCheck.D.canceled += CancelD;
        CallBubbleFlyEvent.OnVoidEventRaised += OnCallBubbleFly;
    }
    private void OnCallBubbleFly()
    {
        if (IsPressD)
        {
            Bubble_Speed_Event.RaisedFloatEvent(1);
            return;
        }
        if (IsPressA)
        {
            Bubble_Speed_Event.RaisedFloatEvent(2);
            return;
        }
        if (IsPressW)
        {
            Bubble_Speed_Event.RaisedFloatEvent(3);
            return;
        }
        if (IsPressS)
        {
            Bubble_Speed_Event.RaisedFloatEvent(4);
        }
    }
    private void OnDisable()
    {
        CallBubbleFlyEvent.OnVoidEventRaised -= OnCallBubbleFly;
        inputControl.Disable();
    }
    private void CancelD(InputAction.CallbackContext context)
    {
        IsPressD = false;
    }

    private void CancelS(InputAction.CallbackContext context)
    {
        IsPressS = false;
    }

    private void CancelA(InputAction.CallbackContext context)
    {
        IsPressA = false;
    }

    private void CancelW(InputAction.CallbackContext context)
    {
        IsPressW = false;
    }

    private void OnD(InputAction.CallbackContext context)
    {
        IsPressD = true;
    }

    private void OnS(InputAction.CallbackContext context)
    {
        IsPressS = true;
    }

    private void OnA(InputAction.CallbackContext context)
    {
        IsPressA = true;
    }

    private void OnW(InputAction.CallbackContext context)
    {
        IsPressW = true;
    }
}
