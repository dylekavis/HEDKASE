using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance;

    [SerializeField] Door[] eligableDoors;

    void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    void OnEnable()
    {
        SwitchManager.Instance.OnAllActivate += OpenEligableDoors;
        SwitchManager.Instance.OnAllDeactivate += CloseEligableDoors;
    }

    void OnDisable()
    {
        SwitchManager.Instance.OnAllActivate -= OpenEligableDoors;
        SwitchManager.Instance.OnAllDeactivate -= CloseEligableDoors;
    }

    void OpenEligableDoors()
    {
        foreach (var door in eligableDoors)
        {
            door.SetOpenState(true);
        }   
    }

    void CloseEligableDoors()
    {
        foreach (var door in eligableDoors)
        {
            door.SetOpenState(false);
        }
    }
}
