using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameNpcSystem
{
    public class NpcWalker : NpcBase
    {
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
    }
}

