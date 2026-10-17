using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameCharacterControllerSystem
{
    public enum _EBodyPartType
    {
        UPPER_BODY,
        LOWER_BODY,
        ARM,
        LEG
    }

    public class TriggerDetector : MonoBehaviour
    {
        public List<Collider> _CollidingParts = new List<Collider>();
        public _EBodyPartType _BodyPartName;

        private Character character;

        private void Awake()
        {
            character = GetComponentInParent<Character>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (character._CharacterRagdoll._RagdollParts.Contains(col))
            {
                return;
            }

            Character c = col.transform.root.GetComponent<Character>();

            if (null == c)
            {
                return;
            }

            if (character == c)
            {
                return;
            }

            if (!_CollidingParts.Contains(col))
            {
                _CollidingParts.Add(col);
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (_CollidingParts.Contains(col))
            {
                _CollidingParts.Remove(col);
            }

            Character c = col.transform.root.GetComponentInParent<Character>();

            if (null == c)
            {
                return;
            }

            if (c == character)
            {
                return;
            }

            if (c._CharacterRagdoll._IsBodyPartHit)
            {
                c._CharacterRagdoll._IsBodyPartHit = false;
            }
        }
    }
}