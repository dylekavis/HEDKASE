using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform headTransform;

    void LateUpdate()
    {
        Vector3 midpoint = (playerTransform.position + headTransform.position) / 2f;
        transform.position = midpoint;
    }
}
