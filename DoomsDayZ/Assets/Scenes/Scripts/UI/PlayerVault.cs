using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameUISystem
{
    public class PlayerVault : MonoBehaviour
    {
        public Character _Owner;
        public List<GameObject> _Items = new List<GameObject>();

        public void _RemoveItems(GameObject item)
        {
            for(int i = 0; i < _Items.Count; i++)
            {
                if(_Items[i] == item)
                {
                    _Items.RemoveAt(i);
                }
            }
        }
    }
}
