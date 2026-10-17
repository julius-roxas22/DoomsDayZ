using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAttackSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameManagersSystem
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> _CurrentAttacks = new List<AttackInfo>();

        private void Update()
        {
            if (_CurrentAttacks.Count == 0)
            {
                return;
            }

            for (int i = _CurrentAttacks.Count - 1; i >= 0; i--)
            {
                AttackInfo info = _CurrentAttacks[i];
                if (null == info._Attacker)
                {
                    Destroy(info.gameObject);
                    _CurrentAttacks.RemoveAt(i);
                }
            }
        }
    }
}

