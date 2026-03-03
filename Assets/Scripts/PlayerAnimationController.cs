using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Camera mainCam;

    bool isWalking;

    void OnEnable()
    {
        PlayerInputManager.Instance.OnMove += HandleMovement;
        PlayerInputManager.Instance.OnMoveCancelled += CancelMovement;
        PlayerInputManager.Instance.OnPlayerLook += HandleLook;
    }

    void OnDisable()
    {
        PlayerInputManager.Instance.OnMove -= HandleMovement;
        PlayerInputManager.Instance.OnMoveCancelled -= CancelMovement;
        PlayerInputManager.Instance.OnPlayerLook -= HandleLook;
    }

    public void HandleMovement(Vector2 moveVector)
    {
        isWalking = true;
        anim.SetBool("isWalking", true);

        anim.SetFloat("AnimMoveX", moveVector.x);
        anim.SetFloat("AnimMoveY", moveVector.y);
    }

    public void CancelMovement()
    {
        isWalking = false;
        anim.SetBool("isWalking", false);

        anim.SetFloat("IdleX", anim.GetFloat("AnimMoveX"));
        anim.SetFloat("IdleY", anim.GetFloat("AnimMoveY"));
    }

    public void HandleLook(Vector2 lookDir)
    {
        if (isWalking)
        {
            anim.SetFloat("AnimMoveX", lookDir.x);
            anim.SetFloat("AnimMoveY", lookDir.y);
        }
        else
        {
            anim.SetFloat("IdleX", lookDir.x);
            anim.SetFloat("IdleY", lookDir.y);
        }
    }
} 
