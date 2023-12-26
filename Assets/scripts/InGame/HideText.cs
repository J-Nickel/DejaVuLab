using TMPro;
using UnityEngine;

namespace InGame
{
    public class HideText : MonoBehaviour
    {
        [SerializeField] private float delay = 5;
        [SerializeField] private float duration = 2f;

        private TMP_Text _text;
        private float _currentFadeTime;
        private bool _isFading;

        public void Start()
        {
            _text = GetComponent<TMP_Text>();
            Invoke(nameof(StartFade), delay);
        }

        private void Update()
        {
            if (!_isFading) return;

            _currentFadeTime += Time.deltaTime;
            var alpha = Mathf.Lerp(1f, 0f, _currentFadeTime / duration);

            var newColor = _text.color;
            newColor.a = alpha;

            _text.color = newColor;

            if (_currentFadeTime >= duration) _isFading = false;
        }

        private void StartFade()
        {
            _currentFadeTime = 0f;
            _isFading = true;
        }
    }
}