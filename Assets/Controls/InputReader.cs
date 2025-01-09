using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Controls.IPlayerActions: built-in interface. This has all the functions for each Input Action that I created.
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    private bool isTargeting = false;

    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
    public Vector2 MovementValue { get; private set; }

    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;
    public event Action RestoreHpEvent;

    private Controls controls;


    private void Start()
    {
        controls = new Controls();

        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        // When the MonoBehaviour gets destroyed, disable PlayerActions
        controls.Player.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // Set IsAttacking value based on the attack input
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        // Set IsBlocking value based on the block input
        if (context.performed)
        {
            IsBlocking = true;
        }
        else if (context.canceled)
        {
            IsBlocking = false;
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        // Invoke DodgeEvent
        DodgeEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        // Invoke JumpEvent
        JumpEvent?.Invoke();
    }

    // There's nothing to do while looking
    public void OnLook(InputAction.CallbackContext context) {}

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read Vector2-type movement value
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        // Toggle (target <-> cancel)
        // If it's not targeting mode,
        if (!isTargeting)
        {
            // Targeting mode
            TargetEvent?.Invoke();
            isTargeting = true;
        }
        // If it's targeting mode,
        else
        {
            // Cancel targeting mode
            CancelEvent?.Invoke();
        }
    }

    public void OnRestoreHp(InputAction.CallbackContext context) 
    {
        if (!context.performed) { return; }

        RestoreHpEvent?.Invoke();
    }
}
