using System;
using UnityEngine;

[RequireComponent(typeof(EyelerAIController))]
public class EyelerAttack : MonoBehaviour
{
    [SerializeField] Collider2D hitbox;
    [SerializeField] EyelerAIController eyelerAI;

    void Start()
    {
        eyelerAI = GetComponent<EyelerAIController>();
    }

    void OnEnable()
    {
        eyelerAI.OnAttackLand += HandleAttack;
        eyelerAI.OnAttackEnd += CancelAttack;
    }

    void OnDisable()
    {
        eyelerAI.OnAttackLand -= HandleAttack;
        eyelerAI.OnAttackEnd -= CancelAttack;
    }

    void HandleAttack()
    {
        if (hitbox.enabled == false)
            hitbox.enabled = true;
    }

    void CancelAttack()
    {
        if (hitbox.enabled == true)
            hitbox.enabled = false;
    }
}
