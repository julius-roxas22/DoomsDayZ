using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using UnityEngine.AI;
using IndieGameManagersSystem;

namespace IndieGameNpcSystem
{
    public enum _ENpcState
    {
        IDLE,
        MOVING,
        RUN,
        ATTACK,
        PATROLLING,
        TURN_LEFT,
        TURN_RIGHT,
        THROW,
        THREE_SIXTY_ATTACK,
        ARGONIZING,
        RUNNING
    }

    public class NpcBase : MonoBehaviour
    {
        [Header("Default AI Setup")]
        [SerializeField] protected Character selfController;
        [SerializeField] protected float survivorRadiusDetector = 8f; //the range radius to detect some survivor
        [SerializeField] protected float chaseRange = 7f;
        [SerializeField] protected float fieldOfView = 90f;
        [SerializeField] protected float attackRange = 0.95f;
        [SerializeField] protected float movementSpeed = 0.25f;
        [SerializeField] protected float radiusPathPatrolling = 6;

        [SerializeField] private float reachedPathStoppingDistance = 0.4f; //stopping distance of agent
        [SerializeField] private bool enablePatrolling = true;
        [SerializeField] private float idleTime = 4;

        [HideInInspector] public bool detectedAlly; // check if something touches other ally 

        private float time = 0f;

        [Space(17)]
        [Header("AI Details")]
        public _ENpcState currentStateActive; // npc current active state
        public GameObject _DetectedTarget; // npc where can able to attack/chase when its not null
        [SerializeField] protected List<Character> survivorsDetected = new List<Character>(); // entry of the survivor detected

        [Space(17)]
        [Header("Components")]
        public NavMeshAgent navAgent;
        public GameObject _Weapon; // zombie weapon; if necessary

        protected virtual void Start() // virtual means a function with pre-implemented logic inside of curly braces
        {
            selfController = GetComponentInParent<Character>();
            navAgent = GetComponentInParent<NavMeshAgent>();
            navAgent.speed = movementSpeed;
        }

        protected virtual void Update()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, survivorRadiusDetector);

            List<Character> currentDetectSurvivors = new List<Character>();

            foreach (Collider col in colliders)
            {
                Character survivor = col.GetComponent<Character>();

                if (null == survivor)
                {
                    continue;
                }

                if (survivor._AllyType == selfController._AllyType)
                {
                    continue;
                }

                if (!currentDetectSurvivors.Contains(survivor))
                {
                    currentDetectSurvivors.Add(survivor);
                }
            }

            foreach (Character control in currentDetectSurvivors)
            {
                if (!survivorsDetected.Contains(control))
                {
                    survivorsDetected.Add(control);
                }
            }

            survivorsDetected.RemoveAll(c => !currentDetectSurvivors.Contains(c));//remove every element of the survivor that is not in current

            targetLocked();
            takeDamageDetected(); // agent will stop for a while once he take damage and other fail safe

            if (null != GameManager.GetInstance._GetGameUI())
            {
                GameManager.GetInstance._GetGameUI()._UpdatePlayerHealthBar(selfController);//update healthbar npc
            }
        }

        private void takeDamageDetected()
        {
            if (selfController._CharacterRagdoll._IsBodyPartHit)
            {
                if (selfController._Animator.applyRootMotion)
                {
                    selfController._Animator.applyRootMotion = false;
                }
                selfController._Animator.transform.localPosition = Vector3.zero;
                selfController._Animator.transform.localRotation = Quaternion.identity;
                navAgent.ResetPath();
            }
        }

        protected Vector3 distanceToPlayer
        {
            get
            {
                if (null != _DetectedTarget)
                {
                    return _DetectedTarget.transform.position - transform.parent.position;
                }
                return Vector3.zero;
            }
        }

        protected bool targetIsAlive
        {
            get
            {
                Character c = _DetectedTarget.GetComponent<Character>();
                if (c._HealthSystem._DefenseData._CurrentHealth > 0f)
                {
                    return true;
                }

                return false;
            }
        }

        protected float targetIsbehind // by checking float value if can rotate left or right < 0 turn left > 0 turn right
        {
            get
            {
                if (distanceToPlayer.sqrMagnitude < attackRange)
                {
                    return Vector3.SignedAngle(transform.parent.forward, distanceToPlayer.normalized, Vector3.up);
                }
                return 0;
            }
        }

        protected bool isFacingTarget
        {
            get
            {
                if (null != _DetectedTarget) // check if target is within in chase range of the npc
                {
                    float angle = Vector3.Angle(transform.parent.forward, distanceToPlayer);

                    if (angle > fieldOfView) // check if not in field of view of NPC
                    {
                        //if (null == _DetectedTarget)
                        //{
                        //}
                        return false;
                    }

                    Vector3 origin = transform.parent.position + Vector3.up * 1.2f;
                    Ray ray = new Ray(origin, distanceToPlayer.normalized);

                    if (Physics.Raycast(ray, out RaycastHit hit, chaseRange))
                    {
                        if (hit.collider.gameObject == _DetectedTarget)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public bool _ReachedPath
        {
            get
            {
                return !navAgent.pathPending && navAgent.remainingDistance < reachedPathStoppingDistance;
            }
        }

        public void _SetRandomPath()
        {
            Vector3 randPath = transform.parent.position + Random.insideUnitSphere * radiusPathPatrolling;

            if (NavMesh.SamplePosition(randPath, out NavMeshHit hit, radiusPathPatrolling, NavMesh.AllAreas))
            {
                navAgent.SetDestination(hit.position);
            }
        }

        public bool _IsBeginToPatrol
        {
            get
            {
                if (enablePatrolling)
                {
                    time += Time.deltaTime;
                    if (time >= idleTime)
                    {
                        time = 0;
                        return true;
                    }
                }
                return false;
            }
        }

        public virtual void _MoveToTarget()// move into the target
        {
            if (null != _DetectedTarget)
            {
                navAgent.SetDestination(_DetectedTarget.transform.position);
            }
        }

        private void targetLocked()
        {
            foreach (Character c in survivorsDetected)
            {
                float dist = (c.transform.position - transform.parent.position).sqrMagnitude;
                if (dist < chaseRange * chaseRange)
                {
                    _DetectedTarget = c.gameObject;
                }
                else
                {
                    _DetectedTarget = null;
                }
            }
        }
    }
}
