using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using IndieGameAttackSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameManagersSystem
{
    public enum _EProjectileType
    {
        AttackInfo,
        Rock
    }

    public class PoolProjectileManager : Singleton<PoolProjectileManager>
    {
        #region old pool system
        //private Dictionary<_EProjectileType, List<GameObject>> poolDictionary = new Dictionary<_EProjectileType, List<GameObject>>();

        //private void setup()
        //{
        //    _EProjectileType[] arr = Enum.GetValues(typeof(_EProjectileType)) as _EProjectileType[];

        //    foreach (_EProjectileType p in arr)
        //    {
        //        if (!poolDictionary.ContainsKey(p))
        //        {
        //            poolDictionary.Add(p, new List<GameObject>());
        //        }
        //    }
        //}

        //public GameObject _GetObject(_EProjectileType poolType)
        //{
        //    if (poolDictionary.Count == 0)
        //    {
        //        setup();
        //    }

        //    List<GameObject> list = poolDictionary[poolType];

        //    GameObject obj = null;

        //    if (list.Count > 0)
        //    {
        //        obj = list[0];
        //        list.RemoveAt(0);
        //    }
        //    else
        //    {
        //        obj = PoolObjectLoader._InstantitiatePoolPrefab(poolType).gameObject;
        //    }
        //    return obj;
        //}

        //public void _AddObject(PoolObject poolObj)
        //{
        //    List<GameObject> list = poolDictionary[poolObj._ProjectileType];
        //    list.Add(poolObj.gameObject);
        //    poolObj.gameObject.SetActive(false);
        //}
        #endregion

        private Dictionary<Character, Dictionary<_EProjectileType, Queue<GameObject>>> dicPoolProjectileTypes = new Dictionary<Character, Dictionary<_EProjectileType, Queue<GameObject>>>();

        private void setupPool(Character character)
        {
            if (!dicPoolProjectileTypes.ContainsKey(character))
            {
                dicPoolProjectileTypes[character] = new Dictionary<_EProjectileType, Queue<GameObject>>();
            }

            _EProjectileType[] arr = Enum.GetValues(typeof(_EProjectileType)) as _EProjectileType[];

            foreach (_EProjectileType proc in arr)
            {
                if (!dicPoolProjectileTypes[character].ContainsKey(proc))
                {
                    dicPoolProjectileTypes[character][proc] = new Queue<GameObject>();
                }
            }
        }

        public GameObject _GetProjectile(Character character, _EProjectileType procType)
        {
            setupPool(character);
            Queue<GameObject> procList = dicPoolProjectileTypes[character][procType];

            if (procList.Count == 0)
            {
                GameObject obj = ProjectileLoader._InstantitiatePoolPrefab(procType).gameObject;
                procList.Enqueue(obj);
                obj.SetActive(false);
            }

            if (procList.Count > 0)
            {
                GameObject obj = procList.Dequeue();
                obj.SetActive(true);
                return obj;
            }

            return null;
        }

        public void _ReturnPool(AttackInfo info, _EProjectileType procType/*GameObject obj*/) //deprecated
        {
            //AttackInfo info = obj.GetComponent<AttackInfo>();
            dicPoolProjectileTypes[info._Attacker][procType].Enqueue(info.gameObject);
            info.gameObject.SetActive(false);
        }

        public void _ReturnPool(Character deployer, PoolObject poolObj)
        {
            //get the specific attacker
            //add to the queue 
            //set active to false
            dicPoolProjectileTypes[deployer][poolObj._ProcType].Enqueue(poolObj.gameObject);
            poolObj.gameObject.SetActive(false);

        }

    }
}
