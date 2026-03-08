using System;
using UnityEngine;

[RequireComponent(typeof(EyelerAIController))]
public class EyelerMovement : MonoBehaviour
{
    [Header("Internal References")]
    [SerializeField] EyelerAIController eyelerAI;

    [Header("Movement Parameters")]
    [SerializeField] float moveSpeed = 10f;
    float minAttackDistance => eyelerAI.MinAttackDistance;

    [Header("Idle Parameters")]
    [SerializeField] float yOscillationSpeed = 2f;
    [SerializeField, Range(0, 1)] float yOscillationHeight;

    EyelerState previousState;
    Rigidbody2D rb;
    Transform targetTransform;

    float idleTimer;
    float startY;

    bool hasTarget;

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eyelerAI = GetComponent<EyelerAIController>();
        startY = transform.position.y;
    }

    void FixedUpdate()
    {
        EyelerState currentState = eyelerAI.GetEyelerState;

        if (currentState != previousState)
        {
            OnStateChanged(currentState);
            previousState = currentState;
        }

        if (eyelerAI.GetEyelerState == EyelerState.Attacking)
        {
            Attack();
        }
        else if (eyelerAI.GetEyelerState == EyelerState.Idle)
        {
            IdleMovement();
        }
        else if (eyelerAI.GetEyelerState == EyelerState.Chasing)
        {
            Chase();
        }
    }

    void OnStateChanged(EyelerState state)
    {
        if (state == EyelerState.Idle)
        {
            startY = transform.position.y;
            idleTimer = 0f;
        }
    }

    void Attack()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void Chase()
    {
        Vector2 movePos = Vector2.MoveTowards(
            rb.position,
            eyelerAI.Target.position,
            moveSpeed * Time.fixedDeltaTime
        );
            
        rb.MovePosition(movePos);
    }

    void IdleMovement()
    {
        idleTimer += Time.deltaTime;

        float sin = Mathf.Sin(yOscillationSpeed * idleTimer) * yOscillationHeight;

        Vector3 idleMove = new Vector3(transform.position.x, startY + sin);

        rb.MovePosition(idleMove);
    }
}
