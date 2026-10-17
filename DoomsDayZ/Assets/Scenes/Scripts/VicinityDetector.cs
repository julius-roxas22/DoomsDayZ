using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:check if the gameobject detects on his vicinity is weapon or not
namespace IndieGameCharacterControllerSystem
{
    //this script is attached into ground detector game object to detect weapon only
    public class VicinityDetector : MonoBehaviour
    {
        public float _RadiusToDetect;
        //[SerializeField] private LayerMask weaponMask;
        public List<Collider> _VicinityDetects = new List<Collider>();
        private Character self;

        private void Awake()
        {
            self = GetComponentInParent<Character>();
        }

        //random pick when characater soround by many weapons
        private void Update()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _RadiusToDetect);

            List<Collider> current = new List<Collider>();

            foreach (Collider col in colliders)
            {
                //if (null != col.transform.parent) // want to check if its stucked on the dead body part
                //{
                //    continue;
                //}

                if (col.gameObject.layer == 6)   //check if it is a ground layer
                {
                    continue;
                }

                if (col.gameObject == self.gameObject)
                {
                    continue;
                }

                if (self._CharacterRagdoll._RagdollParts.Contains(col))
                {
                    continue;
                }

                if (!current.Contains(col))
                {
                    current.Add(col);
                }
            }

            foreach (Collider col in current)
            {
                //if (null != col.transform.parent) // want to check if its stucked on the dead body part
                //{
                //    continue;
                //}

                if (col.gameObject.layer == 6)   //check if it is a ground layer
                {
                    continue;
                }

                if (self._CharacterRagdoll._RagdollParts.Contains(col))
                {
                    continue;
                }

                if (!_VicinityDetects.Contains(col))
                {
                    _VicinityDetects.Add(col);
                }
            }

            _VicinityDetects.RemoveAll(c => !current.Contains(c));
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, _RadiusToDetect);
        //}
    }
}
