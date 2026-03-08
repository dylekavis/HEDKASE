using System;
using System.Collections;
using UnityEngine;

public enum EyelerState
{
    Idle,
    Chasing,
    Attacking
}

public class EyelerAIController : MonoBehaviour
{
    public event Action<Vector2> OnChaseStart;
    public event Action OnChaseEnd;
    public event Action OnAttackAttempt;
    public event Action OnAttackLand;
    public event Action OnAttackEnd;

    [Header("Eyeler State")]
    [SerializeField] EyelerState state;

    [Header("Detection Parameters")]
    [SerializeField] DetectionRadius detectionRadius;
    [SerializeField] float detectionResetTime = 2f;

    [Header("Attacking Parameters")]
    [SerializeField] float minDistanceToAttemptAttack = 1.25f;
    [SerializeField] float attackAttemptTime = 0.12f;
    [SerializeField] float attackCooldownTime = 2f;

    bool canAttack = true;

    Transform target;
    Coroutine attackAttempt;
    Coroutine playerDetect;

    void OnEnable()
    {
        detectionRadius.OnPlayerDetected += SetTargetToChase;
        detectionRadius.OnPlayerUndetected += CancelChase;
    }

    void OnDisable()
    {
        detectionRadius.OnPlayerDetected -= SetTargetToChase;
        detectionRadius.OnPlayerUndetected -= CancelChase;
    }

    void Update()
    {
        if (state != EyelerState.Chasing && state != EyelerState.Attacking)
            return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= minDistanceToAttemptAttack && canAttack && attackAttempt == null)
        {
            canAttack = false;
            state = EyelerState.Attacking;

            attackAttempt = StartCoroutine(AttackAttemptCounter());
        }
    }

    void SetTargetToChase(GameObject player)
    {
        target = player.transform;

        state = EyelerState.Chasing;
        OnChaseStart?.Invoke(target.position);
    }

    void CancelChase()
    {
        state = EyelerState.Idle;
        OnChaseEnd?.Invoke();
    }

    IEnumerator AttackAttemptCounter()
    {
        OnAttackAttempt?.Invoke();

        yield return new WaitForSeconds(attackAttemptTime);

        if (target == null)
        {
            ResetAttack();
            yield break;
        }

        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= minDistanceToAttemptAttack)
            {
                OnAttackLand?.Invoke();
            }
        }

        yield return new WaitForSeconds(attackCooldownTime);

        ResetAttack();
    }


    void ResetAttack()
    {   
        state = target != null ? EyelerState.Chasing : EyelerState.Idle;

        OnAttackEnd?.Invoke();
        canAttack = true;

        attackAttempt = null;
    }

    public EyelerState GetEyelerState => state;
    public float MinAttackDistance => minDistanceToAttemptAttack;
    public Transform Target => target;
}
