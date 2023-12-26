using System;
using UnityEngine;

namespace InGame
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private DestroyRule destroyRule = DestroyRule.NoDestroy;
        [SerializeField] private float delay = .5f;
        [SerializeField] private int contactThreshold = 3;

        private int _contactCount;

        private void OnTriggerEnter2D(Collider2D c)
        {
            if (c == null || !c.CompareTag("Player")) return;
            switch (destroyRule)
            {
                case DestroyRule.Delay:
                    Invoke(nameof(SelfDestroy), delay);
                    break;
                case DestroyRule.Contact:
                    _contactCount += 1;
                    if (_contactCount >= contactThreshold) SelfDestroy();
                    break;
                case DestroyRule.NoDestroy:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private enum DestroyRule
        {
            Delay,
            Contact,
            NoDestroy
        }
    }
}