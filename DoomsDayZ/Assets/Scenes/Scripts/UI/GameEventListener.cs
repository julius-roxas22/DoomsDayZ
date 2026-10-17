using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameGameControllerSystem
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent _GameEvent; // reference and to register into gameEvent listener list
        [Space(17)]
        public UnityEngine.Events.UnityEvent response;

        private void Start()
        {
            if (null != _GameEvent)
            {
                _GameEvent._AddListener(this);
            }
        }

        public void _OnRaisedEvent()
        {
            response.Invoke();
        }
    }
}

