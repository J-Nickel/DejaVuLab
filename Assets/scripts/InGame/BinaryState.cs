using UnityEngine;

namespace InGame
{
    public class BinaryState : MonoBehaviour
    {
        [SerializeField] protected bool isActive;
        [SerializeField] private bool detectPlayer;
        [SerializeField] private bool detectCursor;
        private bool _isBlocked;
        private Animator _animator;
        private static readonly int Active = Animator.StringToHash("active");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            ShowState();
        }

        private void OnMouseDown()
        {
            if (!detectCursor) return;
            if (_isBlocked) return;
            SwitchState();
        }

        private void OnTriggerEnter2D(Collider2D c)
        {
            if (!detectPlayer || c == null || !c.CompareTag("Player")) return;
            if (_isBlocked) return;
            SwitchState();
        }

        public void SwitchState()
        {
            isActive = !isActive;
            ShowState();
        }

        private void ShowState()
        {
            _animator.SetBool(Active, isActive);
        }
        
        public bool IsBlocked
        {
            set => _isBlocked = value;
        }
    }
}