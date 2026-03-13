using System;
using System.Collections;
using UnityEngine;

public class PlayerPitDetection : MonoBehaviour
{
    public event Action OnPitDetected;
    public event Action<Vector2> OnRespawnCreated;

    PlayerMovement pm;

    Vector2 respawnPoint;

    void Awake()
    {
        pm = GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pit"))
        {
            OnPitDetected?.Invoke();
            Debug.Log($"{name} detected the pit");

            Vector2 moveDir = pm.GetMoveInput.normalized;

            respawnPoint = (Vector2)transform.position - moveDir * 1.5f;

            StartCoroutine(RespawnDelay());
        }
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(0.4f);

        OnRespawnCreated?.Invoke(respawnPoint);
    }
}
