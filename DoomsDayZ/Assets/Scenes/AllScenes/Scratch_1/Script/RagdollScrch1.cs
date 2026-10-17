using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScrch1 : MonoBehaviour
{
    public List<Collider> _BodyPartsCollider = new List<Collider>();

    private void Awake()
    {

    }

    public void _RagdollSetup()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            if (col.gameObject == gameObject)
            {
                continue;
            }

            if (!_BodyPartsCollider.Contains(col))
            {
                col.isTrigger = true;
                _BodyPartsCollider.Add(col);
            }
        }
    }
}
