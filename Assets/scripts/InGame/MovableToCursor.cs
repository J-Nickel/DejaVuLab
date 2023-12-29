using UnityEngine;

namespace InGame
{
    public class MovableToCursor : MonoBehaviour
    {
        [SerializeField] private bool invert;
        [SerializeField] private float attractionForce = -5.0f;
        private Rigidbody2D _rb;
        private Camera _cam;

        private void Start()
        {
            _cam = Camera.main;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!Input.GetMouseButton(0)) return;
            var mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            var direction = (mousePosition - transform.position).normalized * (invert ? -1 : 1);
            _rb.AddForce(attractionForce * direction);
        }
    }
}