using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InGame
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float timeLimit;
        private Image _timeBar;
        private float _timeLeft;

        private void Start()
        {
            _timeBar = GetComponent<Image>();
            _timeLeft = timeLimit;
        }

        private void Update()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                _timeBar.fillAmount = _timeLeft / timeLimit;
            }
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}