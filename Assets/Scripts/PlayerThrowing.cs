using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{
    [SerializeField] GameObject headObject;
    [SerializeField] GameObject otherObject;
    [SerializeField] float maxChargeTime = 3f;
    [SerializeField] float throwForce = 10f;
    
    float currentCharge;

    bool hasHead = true;

    Coroutine throwCharge;

    void OnEnable()
    {
        PlayerInputManager.Instance.OnThrow += StartCharge;
        PlayerInputManager.Instance.OnThrowCancelled += CancelThrow;
    }

    void OnDisable()
    {
        PlayerInputManager.Instance.OnThrow -= StartCharge;
        PlayerInputManager.Instance.OnThrowCancelled -= CancelThrow;
    }

    void StartCharge()
    {
        if (throwCharge != null) return;

        throwCharge = StartCoroutine(ChargeThrow());
    }

    void CancelThrow(Vector2 aimDirection)
    {
        if (throwCharge != null)
        {
            StopCoroutine(throwCharge);
            ThrowObject(aimDirection);
            throwCharge = null;
        }
    }

    IEnumerator ChargeThrow()
    {
        currentCharge = 0f;

        while (currentCharge < maxChargeTime)
        {
            currentCharge += Time.deltaTime;
            yield return null;
        }

        currentCharge = maxChargeTime;
    }

    void ThrowObject(Vector2 aimDirection)
    {
        GameObject objectToThrow = null;

        if (hasHead)
        {
            objectToThrow = headObject;
            hasHead = false;
        }
        else if (otherObject != null)
        {
            objectToThrow = otherObject;
        }
        else return;

        float chargePercent = currentCharge / maxChargeTime;
        float finalForce = chargePercent * throwForce;
        Vector2 spawnOffset = aimDirection.normalized * 0.8f;
        Vector2 spawnPos = (Vector2)transform.position + spawnOffset;

        GameObject thrown = Instantiate(objectToThrow, spawnPos, Quaternion.identity);
        Rigidbody2D thrownRb = thrown.GetComponent<Rigidbody2D>();

        thrownRb.AddForce(aimDirection * finalForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Head"))
        {
            headObject = collision.gameObject; //fix for object pooling
            hasHead = true;
        }
    }
}
