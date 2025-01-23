using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressCheck : MonoBehaviour
{
    private PlayerInputControl inputControl;
    [Header("¼ì²â°´¼ü")]
    public bool IsPressW;
    public bool IsPressA;
    public bool IsPressS;
    public bool IsPressD;
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
    }
    private void OnDisable()
    {
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
