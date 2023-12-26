using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace InGame
{
    internal enum MouseAction
    {
        Up,
        Down
    }

    [Serializable]
    internal class MouseEvent
    {
        [SerializeField] private MouseAction action;
        [SerializeField] private float minDelay;
        [SerializeField] private float maxDelay;
        [SerializeField] private string text;

        public MouseEvent(MouseAction action, float minDelay, float maxDelay, string text)
        {
            this.action = action;
            this.minDelay = minDelay;
            this.maxDelay = maxDelay;
            this.text = text;
        }

        public MouseAction Action => action;

        public float MinDelay => minDelay;

        public float MaxDelay => maxDelay;

        public string Text => text;
    }

    public class ClickListener : MonoBehaviour
    {
        [SerializeField] private List<MouseEvent> requireEvents;
        [SerializeField] private GameObject finish;
        [SerializeField] private GameObject display;
        private List<MouseEvent> _events;
        private float _lastTime;

        private void Start()
        {
            finish.GetComponent<CircleCollider2D>().enabled = false;
            _lastTime = Time.time;
            _events = new List<MouseEvent>();
        }

        private void OnMouseDown()
        {
            RegisterAction(MouseAction.Down);
        }

        private void OnMouseUp()
        {
            RegisterAction(MouseAction.Up);
        }

        private void RegisterAction(MouseAction ma)
        {
            if (_events.Count >= requireEvents.Count) _events.RemoveAt(0);
            var time = Time.time;
            _events.Add(new MouseEvent(ma, Math.Abs(_lastTime - time), 0f, null));
            _lastTime = time;

            if (!ValidateActionList()) return;
            finish.GetComponent<BinaryState>().SwitchState();
            var bc = finish.GetComponent<BoxCollider2D>();
            if (bc != null) bc.enabled = false;
            finish.GetComponent<CircleCollider2D>().enabled = true;
        }

        private bool ValidateActionList()
        {
            var res = true;
            StringBuilder builder = null;
            if (display != null)
            {
                display.GetComponent<TMP_Text>().text = "";
                builder = new StringBuilder();
            }

            
            for (var i = 0; i < requireEvents.Count && i < _events.Count; i++)
            {
                if (_events[i].Action == requireEvents[i].Action &&
                    _events[i].MinDelay >= requireEvents[i].MinDelay &&
                    _events[i].MinDelay <= requireEvents[i].MaxDelay)
                {
                    builder?.Append(requireEvents[i].Text == null ? "" : requireEvents[i].Text);
                }
                else
                {
                    res = false;
                    break;
                }

                if (builder == null) continue;
                Debug.Log(builder.ToString());
                display.GetComponent<TMP_Text>().text = builder.ToString();
            }
            
            if (!res) _events.Clear();
            return _events.Count == requireEvents.Count && res;
        }
    }
}