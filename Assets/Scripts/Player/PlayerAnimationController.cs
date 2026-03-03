using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Camera mainCam;

    bool isWalking = false;

    void OnEnable()
    {
        PlayerInputManager.Instance.OnMove += HandleMovement;
        PlayerInputManager.Instance.OnMoveCancelled += CancelMovement;
        PlayerInputManager.Instance.OnPlayerLook += HandleLook;
        PlayerInputManager.Instance.OnLookCancelled += CancelLook;
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
        anim.SetFloat("IdleX", lookDir.x);
        anim.SetFloat("IdleY", lookDir.y);
        anim.SetFloat("LastLookX", lookDir.x);
        anim.SetFloat("LastLookY", lookDir.y);
    }

    public void CancelLook()
    {
        anim.SetFloat("IdleX", anim.GetFloat("LastLookX"));
        anim.SetFloat("IdleY", anim.GetFloat("LastLookY"));
    }
} 
