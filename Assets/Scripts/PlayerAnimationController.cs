using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Camera mainCam;

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
        //MUST ADD BOOL FOR WALKING AND ROLLING
        anim.SetFloat("IdleX", moveVector.x);
        anim.SetFloat("IdleY", moveVector.y);
    }

    public void CancelMovement()
    {
        //MUST ADD BOOL AS ABOVE
        anim.SetFloat("IdleX", anim.GetFloat("IdleX")); //change 2nd argument to "AnimMoveX
        anim.SetFloat("IdleY", anim.GetFloat("IdleY")); //AnimMoveY when walking implemented
    }
} 
