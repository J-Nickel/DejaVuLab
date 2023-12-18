using UnityEngine;

public class Platform_pre : MonoBehaviour
{
    [SerializeField] private bool destroyable;
    [SerializeField] private float delay = .5f;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!destroyable) return;
        if (c == null || !c.CompareTag("Player")) return;
        Invoke(nameof(SelfDestroy), delay);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}