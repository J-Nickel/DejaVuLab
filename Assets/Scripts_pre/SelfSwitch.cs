using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSwitch : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private bool detectPlayer;
    [SerializeField] private bool detectCursor;

    [SerializeField] private Sprite inactive;
    [SerializeField] private Sprite active;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        ShowState();
    }

    private void OnMouseDown()
    {
        if (!detectCursor) return;
        SwitchState();
    }
    
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!detectPlayer || c == null || !c.CompareTag("Player")) return;
        SwitchState();
    }

    public void SwitchState()
    {
        isActive = !isActive;
        ShowState();
    }

    private void ShowState()
    {
        _renderer.sprite = isActive ? active : inactive;
    }
}