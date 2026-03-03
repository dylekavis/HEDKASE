using UnityEngine;

public class HeadCollection : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(collision.gameObject.transform);
        }
    }
}
