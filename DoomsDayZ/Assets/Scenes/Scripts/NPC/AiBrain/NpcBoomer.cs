using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameAudioSystem;

namespace IndieGameNpcSystem
{
    public class NpcBoomer : NpcBase
    {
        [Space(17)]
        [Header("Ability")]
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionImpact;
        [SerializeField] private int explosionDamage;

        private bool isExploded;

        [HideInInspector] public bool isPathCalculated;
        [HideInInspector] public Vector3 currentPositionSeen;
        [HideInInspector] public Vector3 lastPositionSeen;

        private AudioSource source;

        protected override void Update()
        {
            base.Update();
            if (null != _DetectedTarget)
            {
                activeState();
            }
            else
            {
                currentStateActive = _ENpcState.PATROLLING;
            }

            if (selfController._HealthSystem._DefenseData._CurrentHealth <= 0f)
            {
                _Exlopde();
            }
        }

        public void _MoveToNewTarget() // being calculated to a new path
        {
            if ((lastPositionSeen - transform.position).sqrMagnitude < 2f)
            {
                currentStateActive = _ENpcState.ARGONIZING;
            }
            navAgent.SetDestination(lastPositionSeen);
        }

        private void activeState()
        {
            if (!targetIsAlive) // keep patroling the other boomer without knowing if player is dead or not
            {
                currentStateActive = _ENpcState.PATROLLING; // patrolling when target is dead
                return;
            }

            if (isFacingTarget)
            {
                if (distanceToPlayer.sqrMagnitude < attackRange)
                {
                    currentStateActive = _ENpcState.ARGONIZING;
                }
                else if (distanceToPlayer.sqrMagnitude < chaseRange)
                {
                    currentStateActive = _ENpcState.RUNNING;
                }
                else
                {
                    currentStateActive = _ENpcState.PATROLLING;
                }
            }
            else
            {
                if (targetIsbehind < 0)
                {
                    currentStateActive = _ENpcState.TURN_LEFT;
                }
                else if (targetIsbehind > 0)
                {
                    currentStateActive = _ENpcState.TURN_RIGHT;
                }
                else
                {
                    currentStateActive = _ENpcState.PATROLLING;
                }
            }
        }

        //just to add some explosion effect and damage impact for those surround on him
        public void _Exlopde()
        {
            if (isExploded)
            {
                return;
            }

            if (null == source)//explotion sound effect
            {
                AudioProgress progress = selfController.GetComponent<AudioProgress>();
                source = AudioManager.GetInstance._GetAudioSource(progress, _EAudioType.DIE);
                source.PlayOneShot(progress._GetClip(_EAudioType.DIE));
            }

            isExploded = true;
            StartCoroutine(selfController._IEvanishDeadBody(0.01f));

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider col in colliders)
            {
                Character character = col.GetComponent<Character>();

                if (null == character || selfController == character) // add a guard here
                {
                    continue;
                }

                if (null != character._CharacterRagdoll)
                {
                    character._CharacterRagdoll._Attacker = selfController;
                    Vector3 impact = (character.transform.position - transform.position).normalized * explosionImpact;
                    character._CharacterRagdoll._DirectionDamageImpact = -impact;
                    character._CharacterRagdoll._TriggerPart = findLowerBodyPart(character);
                }

                if (null != character._DamageDetector)
                {
                    character._DamageDetector._TakeDamage(character, explosionDamage);
                }
            }
        }

        private TriggerDetector findLowerBodyPart(Character character)
        {
            foreach (Collider col in character._CharacterRagdoll._RagdollParts)
            {
                TriggerDetector trigger = col.GetComponent<TriggerDetector>();
                if (null == trigger)
                {
                    continue;
                }

                if (trigger._BodyPartName == _EBodyPartType.LOWER_BODY || trigger._BodyPartName == _EBodyPartType.LEG)
                {
                    return trigger;
                }
            }
            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}