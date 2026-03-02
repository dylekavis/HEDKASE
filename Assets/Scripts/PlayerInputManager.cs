using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    public event Action<Vector2> OnMove;
    public event Action OnMoveCancelled;

    Vector2 moveInput;

    void Awake()
    {
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
}
