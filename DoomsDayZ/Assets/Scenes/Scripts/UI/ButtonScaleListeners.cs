using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameGameControllerSystem
{

    public enum ButtonScaleType
    {
        ButtonScaleUp,
    }

    public class ButtonScaleListeners : MonoBehaviour
    {
        private Dictionary<GameObject, Animator> dicButton = new Dictionary<GameObject, Animator>();
        private GameEventListener listener;

        private void Awake()
        {
            listener = GetComponent<GameEventListener>();
        }

        public void _OnScaleUp()
        {
            getAnimator(listener._GameEvent._EventObject).SetBool(ButtonScaleType.ButtonScaleUp.ToString(), true);
        }

        public void _OnScaleDefault()
        {
            getAnimator(listener._GameEvent._EventObject).SetBool(ButtonScaleType.ButtonScaleUp.ToString(), false);
        }

        private Animator getAnimator(GameObject eventObj)
        {
            if (!dicButton.ContainsKey(eventObj))
            {
                Animator anim = eventObj.GetComponent<Animator>();
                dicButton.Add(eventObj, anim);
                return anim;
            }
            else
            {
                return dicButton[eventObj];
            }
        }
    }
}