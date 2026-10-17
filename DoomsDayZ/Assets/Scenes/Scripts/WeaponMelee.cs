using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameAudioSystem;
using IndieGameManagersSystem;

namespace IndieGameWeaponSystem
{
    public class WeaponMelee : WeaponBase
    {
        [Header("Weapon Data")]
        public WeaponMeleeData _WeaponMeleeData;

        [Space(17)]
        [Header("Basic Details Setup")]
        [SerializeField] private float reachedEndDistanceForce;

        [HideInInspector] public Vector3 startPointPosition; // this variable is starting point of position when the weapon is throw --
        [HideInInspector] public BoxCollider _BoxCollider;

        private AudioProgress progress;
        private AudioSource throwSource;
        private AudioSource collidedSource;
 //       private WeaponMeleeData weaponMeleedata;
        private int throwDamage;

        public override void Awake()
        {
            _BoxCollider = GetComponent<BoxCollider>();
            progress = GetComponent<AudioProgress>();
        }

        public override void Update()
        {
            if (_IsThrowing)
            {
                // just to check the distance between the melee weapon and max travel distance
                // just keep moving and rotating the melee weapon 
                if ((startPointPosition - transform.position).sqrMagnitude <= maxDistance)
                {
                    transform.position += _MoveDirection * travelSpeed * Time.deltaTime;
                    transform.Rotate(rotateDirection * Time.deltaTime);
                }
                else
                {
                    //stop moving and rotating when melee weapon is reached the max travel distance
                    _RigidBody.isKinematic = false;
                    _BoxCollider.isTrigger = false;
                    _IsThrowing = false;
                    _RigidBody.AddForce(_MoveDirection * reachedEndDistanceForce);
                }

                throwSource = AudioManager.GetInstance._GetAudioSource(progress, _EAudioType.THROW);
                throwSource.clip = progress._GetClip(_EAudioType.THROW);
                throwSource.pitch = 2.5f; // how fast the sound effect
                throwSource.loop = true;
                if (!throwSource.isPlaying)
                {
                    throwSource.Play();
                }
            }
            else
            {
                if (null != throwSource && throwSource.isPlaying)
                {
                    throwSource.Stop();
                }
            }

            //show up the the detailed
            if (null != _Light)
            {
                _Light.enabled = transform.parent == null;
                _WeaponContainer.gameObject.SetActive(transform.parent == null);
            }
        }

        public override void OnTriggerEnter(Collider col)
        {
            if (!_IsThrowing) // take only damage when its throwing
            {
                return;
            }

            foreach (string ignore in ignoreCollisions)//ignore entry of object in the list when its collided
            {
                if (col.gameObject.CompareTag(ignore))
                {
                    _BoxCollider.isTrigger = false;
                    _RigidBody.isKinematic = false;
                    _IsThrowing = false;
                }
            }

            Character ownerTriggered = col.transform.root.GetComponent<Character>();

            if (null == ownerTriggered)
            {
                return;
            }

            if (ownerTriggered.gameObject == _Attacker.gameObject)
            {
                return;
            }

            Collider collidedPart = ownerTriggered._CharacterRagdoll._GetCollidedPart(col);

            if (null == collidedPart)
            {
                return;
            }

            if (isCollided)
            {
                return;
            }

            if (_EnabledStucked)
            {
                _BoxCollider.isTrigger = true;
                transform.parent = collidedPart.transform;
                transform.localPosition = collidedPart.transform.localPosition + piercePosition;
                transform.localRotation = Quaternion.LookRotation(_Attacker.transform.right);
                //    ownerTriggered._CharacterRagdoll.setupRagdollParts(); // re set the character ragdoll parts when the weapon stucked on the limbs
            }
            else
            {
                _BoxCollider.isTrigger = false;
                _RigidBody.isKinematic = false;
            }

            ownerTriggered._HealthSystem._WeaponProjectileStuck = gameObject;// just to add into weapon stuck projectile; just to add a fail safe when dead body vanished
            isCollided = true;
            _IsThrowing = false;
            throwDamage = Random.Range(_WeaponMeleeData._Damage, _WeaponMeleeData._Damage + 20);
            ownerTriggered._DamageDetector._TakeDamage(new Vector3(_XForce, _YForce, _ZForce), _Attacker, col, throwDamage);

            //put the audio source here
            collidedSource = AudioManager.GetInstance._GetAudioSource(progress, _EAudioType.COLLIDE);
            collidedSource.clip = progress._GetClip(_EAudioType.COLLIDE);
            if (!collidedSource.isPlaying)
            {
                collidedSource.Play();
            }
        }

        public void _ResetWeaponData()
        {
            _Attacker = null;
            isCollided = false;
        }
    }
}

