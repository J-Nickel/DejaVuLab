using UnityEngine;

namespace InGame
{
    public class DnD : MonoBehaviour
    {
        private bool _isDragging;
        private Vector3 _offset;
        private Vector3 _objOffset;

        private void OnMouseDown()
        {
            var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _offset = transform.position - mp;
            _isDragging = true;
            if (_obj != null) _objOffset = _obj.transform.position - mp;
        }

        private void OnMouseUp()
        {
            _isDragging = false;
        }

        private void Update()
        {
            if (!_isDragging) return;
            var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mp.x + _offset.x, mp.y + _offset.y, transform.position.z);

            if (_obj != null)
                _obj.transform.position = new Vector3(
                    mp.x + _objOffset.x,
                    mp.y + _objOffset.y,
                    _obj.transform.position.z
                );
        }

        private GameObject _obj;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            _obj = collision.gameObject;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                _obj = null;
        }
    }
}