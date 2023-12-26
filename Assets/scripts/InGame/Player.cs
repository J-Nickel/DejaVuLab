using UnityEngine;

namespace InGame
{
    public class Player : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private float speed = 3f;
        [SerializeField] private float jumpForce = 6f;
        [SerializeField] private float superJumpForce = 8f;
        [SerializeField] private float delay = 0f;

        [Header("Inversion")] [SerializeField] private bool invertX;
        [SerializeField] private bool invertG;

        [Header("Rules")] [SerializeField] private bool jumpSwitchG;
        [SerializeField] private bool lockControl;
    
        private Rigidbody2D _body;
        private SpriteRenderer _renderer;
        private Animator _animator;

        private bool _useSuperJump;
        private bool _isInvertedG;
        private static readonly int State = Animator.StringToHash("state");

        private void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            if (invertG) InvertG();
        }

        private void Update()
        {
            if (lockControl) return;
            UpdMovement();
            UpdJump();
        }

        private void UpdMovement()
        {
            var axis = Input.GetAxis("Horizontal") * (invertX ? -1 : 1);
            _animator.SetInteger(State, (int)(axis == 0 ? PlayerState.Idle : PlayerState.Run));
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
            if (Mathf.Abs(_body.velocity.y) > 0.05f)
                _animator.SetInteger(State, (int)PlayerState.Jump);
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                _animator.SetInteger(State, (int)PlayerState.Idle);
                if (jumpSwitchG)
                {
                    invertG = !invertG;
                    InvertG();
                }
                else
                    _body.AddForce(new Vector2(0, _useSuperJump ? superJumpForce : jumpForce), ForceMode2D.Impulse);
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

        public void LockControl()
        {
            lockControl = true;
        }

        public void UnlockControl()
        {
            lockControl = false;
        }

        private enum PlayerState
        {
            Idle,
            Run,
            Jump
        }
    }
}
/*
private void Update()
        {
            if (lockControl) return;

            if (Input.GetKeyDown(KeyCode.D))
            {
                _axis = invertX ? -1 : 1;
                Invoke(nameof(Move), delay);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _axis = invertX ? 1 : -1;
                Invoke(nameof(Move), delay);
            }
            
            UpdJump();
        }

        private float _axis;

        private void Move()
        {
            _animator.SetInteger(State, (int)(_axis == 0 ? PlayerState.Idle : PlayerState.Run));
            transform.position += new Vector3(_axis, 0, 0) * (speed * Time.deltaTime);
            _renderer.flipX = _axis switch
            {
                < 0f => true,
                > 0f => false,
                _ => _renderer.flipX
            };
        }
*/