using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float moveSpeed;

    bool isMoving;

    void OnEnable()
    {
        PlayerInputManager.Instance.OnMove += HandleMovement;
        PlayerInputManager.Instance.OnMoveCancelled += CancelMovement;
    }

    void OnDisable()
    {
        PlayerInputManager.Instance.OnMove -= HandleMovement;
        PlayerInputManager.Instance.OnMoveCancelled -= CancelMovement;
    }

    public void HandleMovement(Vector2 moveVector)
    {
        isMoving = true;

        if (isMoving)
            rigidBody.linearVelocity = moveVector * moveSpeed;
    }

    public void CancelMovement()
    {
        isMoving = false;

        rigidBody.linearVelocity = Vector2.zero;
    }


}
