using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;

namespace IndieGameAttackSystem
{
    public class ProjectileLoader : MonoBehaviour
    {
        public static PoolObject _InstantitiatePoolPrefab(_EProjectileType procType)
        {
            GameObject obj = Instantiate(Resources.Load(procType.ToString(), typeof(GameObject))) as GameObject;
            //GameObject obj = null;
            //switch (procType)
            //{
            //    case _EProjectileType.AttackInfo:
            //        {
            //            obj = Instantiate(Resources.Load(procType.ToString(), typeof(GameObject))) as GameObject;
            //            break;
            //        }
            //    case _EProjectileType.Rock:
            //        {
            //            obj = Instantiate(Resources.Load(procType.ToString(), typeof(GameObject))) as GameObject;
            //            break;
            //        }
            //}

            return obj.GetComponent<PoolObject>();
        }
    }
}
