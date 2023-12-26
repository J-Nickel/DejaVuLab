using UnityEngine;

namespace InGame
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private GameObject obj;

        private void OnTriggerEnter2D(Collider2D c)
        {
            if (c == null || !c.CompareTag("Player")) return;
            Destroy(obj);
        }
    }
}