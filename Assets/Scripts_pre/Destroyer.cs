using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruyer : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c == null || !c.CompareTag("Player")) return;
        Destroy(obj);
    }
}