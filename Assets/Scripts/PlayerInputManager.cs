using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    public event Action<Vector2> OnMove;
    public event Action OnMoveCancelled;
    public event Action OnSprint;
    public event Action OnSprintCancelled;
    public event Action OnThrow;
    public event Action<Vector2> OnThrowCancelled;
    public event Action<Vector2> OnPlayerLook;

    [SerializeField] Camera mainCam;

    Vector2 moveInput;
    Vector2 aimDirection;

    void Awake()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;

        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void HandleMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            moveInput = ctx.ReadValue<Vector2>();
            OnMove?.Invoke(moveInput);
        }
        else if (ctx.canceled)
        {
            moveInput = Vector2.zero;
            OnMoveCancelled?.Invoke();
        }
    }

    public void HandleDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnSprint?.Invoke();   
        }
        else if (ctx.canceled)
            OnSprintCancelled?.Invoke();
    }

    public void HandleThrow(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            OnThrow?.Invoke();
        else if (ctx.canceled)
            OnThrowCancelled?.Invoke(aimDirection);
    }

    public void HandleLookDirection(InputAction.CallbackContext ctx)
    {
        Vector2 lookDirection = ctx.ReadValue<Vector2>();

        if (ctx.control.device is Gamepad)
        {
            if (lookDirection.magnitude > 0.01f)
            {
                aimDirection = lookDirection.normalized;
                OnPlayerLook?.Invoke(aimDirection);
            }
        }
        else
        {
            Vector3 mouseScreenPos = lookDirection;
            mouseScreenPos.z = Mathf.Abs(mainCam.transform.position.z);

            Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);
            Vector2 direction = mouseWorldPos - transform.position;

            aimDirection = direction.normalized;
            OnPlayerLook?.Invoke(aimDirection);
        }
    }
}
