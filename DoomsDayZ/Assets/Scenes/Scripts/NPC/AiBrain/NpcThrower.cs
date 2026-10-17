using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameNpcSystem
{
    public class NpcThrower : NpcBase
    {
        //npc project build
        //thrower must have object to throw
        //much faster than the walker
        //high hp
        //heavy attacks
        [Space(17)]
        [Header("Ability")]
        [SerializeField] private float insigthRange = 17;
        [SerializeField] private float backSenses = 5f;
        [SerializeField] private float throwCooldown;
        [SerializeField] private bool canThrow = false;
        private float runningTime = 0f;
        protected override void Start()
        {
            base.Start();
        }

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
        }

        private void activeState()
        {
            //ability cooldown
            if (!canThrow)
            {
                if (runningTime >= throwCooldown)
                {
                    canThrow = true;
                }
                else
                {
                    runningTime += Time.deltaTime;
                }
            }

            if (!targetIsAlive)
            {
                currentStateActive = _ENpcState.PATROLLING; // patrolling when target is dead
                return;
            }

            if (isFacingTarget)
            {
                if (distanceToPlayer.sqrMagnitude < attackRange)
                {
                    currentStateActive = _ENpcState.ATTACK;
                }
                else if (distanceToPlayer.sqrMagnitude < chaseRange)
                {
                    currentStateActive = _ENpcState.MOVING;
                }
                else if (distanceToPlayer.sqrMagnitude < insigthRange)
                {
                    if (canThrow)
                    {
                        currentStateActive = _ENpcState.THROW;
                        runningTime = 0f;
                        canThrow = false;
                    }
                }
                else
                {
                    currentStateActive = _ENpcState.PATROLLING;
                }
            }
            else
            {
                if (/*distanceToPlayer.sqrMagnitude < backSenses*/canThreeSixtyAttack())//check when its no any obstacles behind the back
                {
                    currentStateActive = _ENpcState.THREE_SIXTY_ATTACK;
                }
                else
                {
                    currentStateActive = _ENpcState.PATROLLING;
                }
            }
        }

        //TODO: need to fix this code
        private bool canThreeSixtyAttack()
        {
            //check if target doesnt have any obstacle with him
            //using raycast it the target

            Vector3 origin = selfController.transform.position + Vector3.up * 1.2f;
            Ray ray = new Ray(origin, distanceToPlayer.normalized);
            if (Physics.Raycast(ray, out RaycastHit hit, backSenses))
            {
                if (hit.collider.gameObject == _DetectedTarget)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

