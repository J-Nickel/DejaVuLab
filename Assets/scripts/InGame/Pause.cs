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
            SetLevelTitleEnabled(!_inPause);
            Time.timeScale = _inPause ? 0f : 1f;
        }

        private static void SetLevelTitleEnabled(bool state)
        {
            var objs = GameObject.FindGameObjectsWithTag("LevelTitle");
            if (objs is not { Length: 1 }) return;
            var ht = objs[0].GetComponent<HideText>();
            ht.InPause = state;
        }
    }
}