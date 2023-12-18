using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Player_pre : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float superJumpForce = 8f;

    [Header("Inversion")] [SerializeField] private bool invertX;
    [SerializeField] private bool invertG;

    [Header("Rules")] [SerializeField] private bool jumpSwitchG;

    private Rigidbody2D _body;
    private SpriteRenderer _renderer;

    private bool _useSuperJump;
    private bool _isInvertedG;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        if (invertG) InvertG();
    }

    private void Update()
    {
        UpdMovement();
        UpdJump();
    }

    private void UpdMovement()
    {
        var axis = Input.GetAxis("Horizontal") * (invertX ? -1 : 1);
        transform.position += new Vector3(axis, 0, 0) * (speed * Time.deltaTime);
        _renderer.flipX = axis switch
        {
            < 0f => true,
            > 0f => false,
            _ => _renderer.flipX
        };
    }

    private void UpdJump()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _useSuperJump = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _useSuperJump = false;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && Mathf.Abs(_body.velocity.y) < 0.05f)
        {
            if (jumpSwitchG)
            {
                invertG = !invertG;
                InvertG();
            }
            else _body.AddForce(new Vector2(0, _useSuperJump ? superJumpForce : jumpForce), ForceMode2D.Impulse);
        }
    }

    private void InvertG()
    {
        var pos = transform.position;
        pos.y += invertG ? .75f : -.75f;
        transform.position = pos;

        _body.gravityScale *= -1;
        jumpForce *= -1;
        superJumpForce *= -1;
        var v = transform.localScale;
        v.y *= -1;
        transform.localScale = v;
    }
}