using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerPitDetection pitDetection;

    Vector2 respawnPoint;

    void Awake()
    {
        pitDetection = GetComponentInChildren<PlayerPitDetection>();
    }

    void OnEnable()
    {
        pitDetection.OnPitDetected += HandlePit;
        pitDetection.OnRespawnCreated += HandleRespawn;
    }

    void OnDisable()
    {
        pitDetection.OnPitDetected -= HandlePit;
        pitDetection.OnRespawnCreated -= HandleRespawn;
    }

    void HandlePit()
    {
        HealthManager hm = GetComponent<HealthManager>();

        hm.Damage(10);
        Debug.Log($"{name} fell in the pit, took 10 points of damage. {hm.GetHealth()} remains.");
    }

    void HandleRespawn(Vector2 spawnPoint)
    {
        transform.position = spawnPoint;
    }
}
