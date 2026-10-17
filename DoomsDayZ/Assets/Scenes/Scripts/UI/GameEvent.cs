using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameGameControllerSystem
{
    public class GameEvent : MonoBehaviour
    {
        private List<GameEventListener> listener = new List<GameEventListener>();
        private GameObject eventObject;

        private void Awake()
        {
            listener.Clear();
        }

        public void _AddListener(GameEventListener listener)
        {
            if (!this.listener.Contains(listener))
            {
                this.listener.Add(listener);
            }
        }

        public GameObject _EventObject
        {
            get
            {
                return eventObject;
            }
        }

        public void _Raised()
        {
            foreach (GameEventListener listener in listener)
            {
                listener._OnRaisedEvent();
            }
        }

        public void _Raised(GameObject eventObj)
        {
            eventObject = eventObj;
            foreach (GameEventListener listener in listener)
            {
                listener._OnRaisedEvent();
            }
        }
    }
}

