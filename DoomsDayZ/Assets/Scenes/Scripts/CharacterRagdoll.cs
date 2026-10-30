using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;
using IndieGameInventorySystem;
using IndieGameAudioSystem;

namespace IndieGameCharacterControllerSystem
{
    public class CharacterRagdoll : MonoBehaviour
    {
        [SerializeField] private float durationToVanished = 2.8f;

        public List<Collider> _RagdollParts = new List<Collider>();
        public BoxCollider _BoxCollider;
        public Animator _Animator;
        public Rigidbody _RigidBody;
        public Character _Character;
        public Character _Attacker;
        //public PressAttack _PressAttackType;
        public Vector3 _DirectionDamageImpact;
        public TriggerDetector _TriggerPart;
        public bool _IsRagdollTriggerd;
        public bool _IsBodyPartHit;

        private AudioProgress progress;
        private AudioSource source;

        private void Awake()
        {
            progress = GetComponent<AudioProgress>();
        }

        public void setupRagdollParts()
        {
            _RagdollParts.Clear();

            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != gameObject)
                {
                    _RagdollParts.Add(c);
                    c.isTrigger = true;

                    if (null == c.GetComponent<TriggerDetector>())
                    {
                        c.gameObject.AddComponent<TriggerDetector>();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (_IsRagdollTriggerd)
            {
                _TurnRagdoll();
                //if there is something stuck weapon on the character body part; weapon should be parent as null not to remove also
                StartCoroutine(_Character._IEvanishDeadBody(durationToVanished));//just to vanish dead body after a few seconds ;)
                _IsRagdollTriggerd = false;
            }
        }

        public void _TurnRagdoll()
        {
            //_Animator.Update(0f);
            //Physics.SyncTransforms();
            //transform.position += Vector3.up * 0.01f;
            //Physics.SyncTransforms();

            _Animator.enabled = false;
            _RigidBody.isKinematic = true;
            _RigidBody.useGravity = false;
            _BoxCollider.enabled = false;
            _Character.enabled = false;
            if (!_Character._SubController.enabled) // controlled by computer
            {
                _Character._NPCBase.navAgent.isStopped = true; // stop the navmesh agent if NPC dead
            }

            foreach (Collider col in _RagdollParts)
            {
                Rigidbody rb = col.attachedRigidbody;
                rb.isKinematic = false;
                rb.useGravity = true;
                //rb.linearVelocity = Vector3.zero;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                col.isTrigger = false;
                col.gameObject.layer = LayerMask.NameToLayer("DeadBody");
            }

            if (null == _Attacker)
            {
                return;
            }

            if (Vector3.zero == _DirectionDamageImpact)
            {
                return;
            }

            if (null == _TriggerPart)
            {
                return;
            }

            Rigidbody triggerBody = _TriggerPart.GetComponent<Rigidbody>();

            if (null == triggerBody)
            {
                return;
            }

            Vector3 impactDamage = _Attacker.transform.TransformDirection(_DirectionDamageImpact);
            triggerBody.AddForce(impactDamage);

            if (_Character._IsEquiped) //check if already carrying weapon
            {
                List<GameObject> weaponList = WeaponManager.GetInstance._GetWeaponList(_Character); // get the weapon system
                if (weaponList.Count > 0)
                {
                    _Character._IsEquiped = false;
                    WeaponMelee projectile = weaponList[0].GetComponent<WeaponMelee>(); // get the first weapon
                    projectile.transform.parent = null; // set the weapon parent position into null, so that the weapon is detached from character player
                    projectile._RigidBody = weaponList[0].AddComponent<Rigidbody>(); // add rigid body physics to interact physical object
                    projectile._RigidBody.isKinematic = false;//to avoid floating
                    projectile._BoxCollider.isTrigger = false;//to keep collide with other object
                    weaponList.RemoveAt(0);// remove the first weapon
                }
                //just to remove the weapon into ragdoll parts and set the weapon layer to weapon layer
                Collider weaponCollider = _RagdollParts[_RagdollParts.Count - 1];
                weaponCollider.gameObject.layer = LayerMask.NameToLayer("Weapon");
                _RagdollParts.RemoveAt(_RagdollParts.Count - 1);
            }

            deathSoundEffect(); //death sound effect method
        }

        private void deathSoundEffect()
        {
            //AudioProgress progress
            source = AudioManager.GetInstance._GetAudioSource(progress, _EAudioType.DIE);
            source.PlayOneShot(progress._GetClip(_EAudioType.DIE));
        }

        public void _RagdollChecker(Character damageTaker)
        {
            if (damageTaker._HealthSystem._DefenseData._CurrentHealth <= 0f)
            {
                if (!damageTaker._CharacterRagdoll._IsRagdollTriggerd)
                {
                    damageTaker._CharacterRagdoll._IsRagdollTriggerd = true;
                }
            }
        }

        //just to check if some body part has been collided
        public Collider _GetCollidedPart(Collider collider)
        {
            foreach (Collider col in _RagdollParts)
            {
                if (col.gameObject == _Character.gameObject)
                {
                    continue;
                }

                if (col.gameObject == collider.gameObject)
                {
                    return col;
                }
            }
            return null;
        }
    }
}
