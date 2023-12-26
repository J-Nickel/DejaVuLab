using UnityEngine;

namespace InGame
{
    public class Pause : MonoBehaviour
    {
        private bool _inPause;

        public void SwitchPause()
        {
            _inPause = !_inPause;
            Apply();
        }

        public void PauseOn()
        {
            _inPause = true;
            Apply();
        }

        public void PauseOff()
        {
            _inPause = false;
            Apply();
        }

        private void Apply()
        {
            Time.timeScale = _inPause ? 0f : 1f;
        }
    }
}