using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator fullBodyAnim;
    [SerializeField] Animator headlessBodyAnim;
    [SerializeField] Camera mainCam;

    bool isWalking = false;

    bool hasHead => GetComponent<PlayerThrowing>().HasHead();

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


        if (hasHead)
        {
            fullBodyAnim.gameObject.SetActive(true);
            headlessBodyAnim.gameObject.SetActive(false);

            fullBodyAnim.SetBool("isWalking", true);

            fullBodyAnim.SetFloat("AnimMoveX", moveVector.x);
            fullBodyAnim.SetFloat("AnimMoveY", moveVector.y);
        }
        else
        {
            fullBodyAnim.gameObject.SetActive(false);
            headlessBodyAnim.gameObject.SetActive(true);

            headlessBodyAnim.SetBool("isWalking", true);

            headlessBodyAnim.SetFloat("AnimMoveX", moveVector.x);
            headlessBodyAnim.SetFloat("AnimMoveY", moveVector.y);
        }
    }

    public void CancelMovement()
    {
        isWalking = false;

        if (hasHead)
        {
            fullBodyAnim.gameObject.SetActive(true);
            headlessBodyAnim.gameObject.SetActive(false);

            fullBodyAnim.SetBool("isWalking", false);

            fullBodyAnim.SetFloat("LastLookX", fullBodyAnim.GetFloat("AnimMoveX"));
            fullBodyAnim.SetFloat("LastLookY", fullBodyAnim.GetFloat("AnimMoveY"));
        }
        else
        {
            fullBodyAnim.gameObject.SetActive(false);
            headlessBodyAnim.gameObject.SetActive(true);

            headlessBodyAnim.SetBool("isWalking", false);

            headlessBodyAnim.SetFloat("LastLookX", headlessBodyAnim.GetFloat("AnimMoveX"));
            headlessBodyAnim.SetFloat("LastLookY", headlessBodyAnim.GetFloat("AnimMoveY"));
        }
    }

    public void HandleLook(Vector2 lookDir)
    {
        if (hasHead)
        {
            fullBodyAnim.gameObject.SetActive(true);
            headlessBodyAnim.gameObject.SetActive(false);

            fullBodyAnim.SetFloat("IdleX", lookDir.x);
            fullBodyAnim.SetFloat("IdleY", lookDir.y);
        }
        else
        {
            fullBodyAnim.gameObject.SetActive(false);
            headlessBodyAnim.gameObject.SetActive(true);

            headlessBodyAnim.SetFloat("IdleX", lookDir.x);
            headlessBodyAnim.SetFloat("IdleY", lookDir.y);
        }
    }

    public void CancelLook()
    {
        if (hasHead)
        {
            fullBodyAnim.gameObject.SetActive(true);
            headlessBodyAnim.gameObject.SetActive(false);

            fullBodyAnim.SetFloat("LastLookX", fullBodyAnim.GetFloat("IdleX"));
            fullBodyAnim.SetFloat("LastLookY", fullBodyAnim.GetFloat("IdleY"));
        }
        else
        {
            fullBodyAnim.gameObject.SetActive(false);
            headlessBodyAnim.gameObject.SetActive(true);

            headlessBodyAnim.SetFloat("LastLookX", headlessBodyAnim.GetFloat("IdleX"));
            headlessBodyAnim.SetFloat("LastLookY", headlessBodyAnim.GetFloat("IdleY"));
        }

    }
} 
