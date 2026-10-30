using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameAudioSystem;

namespace IndieGameInventorySystem
{
    public class WeaponHeavy : WeaponBase
    {
        [Header("Basic Details Setup")]
        [SerializeField] private float areaOnEffect;
        [SerializeField] private float impactForce;
        [SerializeField] private int damage;
        [SerializeField] private HashSet<Character> hitCharacter = new HashSet<Character>();

        private BoxCollider boxCollider;
        private Coroutine routine;
        private AudioSource source;
        private AudioProgress audioProgress;

        public override void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
            audioProgress = GetComponent<AudioProgress>();
        }

        public override void Update()
        {
            if (_IsThrowing)
            {
                transform.position += (_MoveDirection - transform.position).normalized * travelSpeed * Time.deltaTime;
                onAOETriggerEnabled(); //check the aoe damage when it throw only
            }
        }

        private void onAOETriggerEnabled()
        {
            //  taking damage of every surrounded in once; not by every frame

            Collider[] colliders = Physics.OverlapSphere(transform.position, areaOnEffect);

            foreach (Collider col in colliders)
            {
                if (_Attacker._CharacterRagdoll._RagdollParts.Contains(col))
                {
                    continue;
                }

                if (col.gameObject == _Attacker.gameObject)
                {
                    continue;
                }

                Character target = col.GetComponentInParent<Character>();

                if (null == target) // it means if this object hit something object excep character
                {
                    continue;
                }

                if (_Attacker == target)
                {
                    continue;
                }

                if (!hitCharacter.Add(target)) // check if target character not added in the list; it will ingnore
                {
                    continue;
                }

                //another issue; when ortiz get take damage from weapon heavy there something horibly wrong in ragdoll parts
                Vector3 impact = (target.transform.position - transform.position).normalized * impactForce;
                Collider getCollider = target._CharacterRagdoll._RagdollParts[Random.Range(0, target._CharacterRagdoll._RagdollParts.Count)];
                target._DamageDetector._TakeDamage(-impact, _Attacker, getCollider, damage);
            }
        }

        public override void OnTriggerEnter(Collider col)
        {
            if (null == source)
            {
                source = AudioManager.GetInstance._GetAudioSource(audioProgress, _EAudioType.ROCK_TO_GROUND_IMPACT);
                AudioClip clip = audioProgress._GetClip(_EAudioType.ROCK_TO_GROUND_IMPACT);
                //source.clip = clip;
                //source.Play();
                source.PlayOneShot(clip);
            }

            if (_Attacker._CharacterRagdoll._RagdollParts.Contains(col)) // check body parts itself
            {
                return;
            }

            if (_Attacker.gameObject == col.gameObject) // check attacker object itself
            {
                return;
            }

            if (null != col.GetComponent<WeaponMelee>()) // check if hit the weapon melee
            {
                return;
            }

            if (distanceAOE(areaOnEffect * 8f))
            {
                CameraManager.GetInstance._TriggerShakeCamera(0.9f);
            }

            _IsThrowing = false;
            Destroy(gameObject);
        }

        public void _TriggeredEnable()
        {
            if (null != routine)
            {
                StopCoroutine(routine);
            }
            else
            {
                routine = StartCoroutine(iETriggerEnable());
            }
        }

        IEnumerator iETriggerEnable()
        {
            yield return new WaitForSeconds(0.3f);
            boxCollider.isTrigger = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, areaOnEffect);
        }

        private bool distanceAOE(float areaOnEffect)
        {
            return (_Attacker._NPCBase._DetectedTarget.transform.position - transform.position).sqrMagnitude < areaOnEffect;
        }
    }
}

