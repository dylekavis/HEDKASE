using System;
using System.Collections;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager Instance;

    public event Action OnAllActivate;
    public event Action OnAllDeactivate;

    [SerializeField] float deactivateTime = 15f;

    [SerializeField] Switch[] connectedSwitches;

    int sentryCount;

    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void RegisterSentry()
    {
        sentryCount++;

        if (sentryCount == connectedSwitches.Length)
            ActivateAll();
    }

    void ActivateAll()
    {
        foreach (var connect in connectedSwitches)
        {
            if (connect != null)
                connect.SetState(SwitchState.Active);
        }

        OnAllActivate?.Invoke();

        StartCoroutine(DeactivateAll());
    }

    IEnumerator DeactivateAll()
    {
        yield return new WaitForSeconds(deactivateTime);

        foreach (var connect in connectedSwitches)
        {
            connect.SetState(SwitchState.Base);
        }

        OnAllDeactivate?.Invoke();
        
        sentryCount = 0;

        yield break;
    }
}
