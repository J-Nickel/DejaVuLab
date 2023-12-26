using UnityEngine;

namespace InGame
{
    public class OpenFinish : MonoBehaviour
    {
        [SerializeField] private bool isActive;
        [SerializeField] private bool invert;
        [SerializeField] private bool detectPlayer;
        [SerializeField] private bool detectCursor;
        [SerializeField] private GameObject finish;
        [SerializeField] private int threshold = 1;

        private int _actions;

        private void Start()
        {
            Apply();
            _actions = 0;
        }

        private void OnMouseDown()
        {
            if (!detectCursor) return;
            RegisterAction();
        }

        private void OnTriggerEnter2D(Collider2D c)
        {
            if (!detectPlayer || c == null || !c.CompareTag("Player")) return;
            RegisterAction();
        }

        private void RegisterAction()
        {
            _actions += 1;
        
            if (_actions < threshold) return;
        
            isActive = !isActive;
            finish.GetComponent<BinaryState>().SwitchState();
            Apply();
            _actions = 0;
        }

        private void Apply()
        {
            finish.GetComponent<CircleCollider2D>().enabled = invert ? !isActive : isActive;
        }
    }
}