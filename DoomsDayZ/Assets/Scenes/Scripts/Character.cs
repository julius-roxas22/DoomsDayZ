using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using IndieGameAnimationStateData;
using IndieGameManagersSystem;
using IndieGameNpcSystem;
using IndieGameHealthSystem;
using IndieGameInventorySystem;
using IndieGameAttackSystem;

//add another weapon with blade
//character should be move a bit backwards if taking damage?
namespace IndieGameCharacterControllerSystem
{
    public enum _EAllyType
    {
        Ally,
        Enemy
    }

    public class Character : MonoBehaviour
    {
        public _ECameraCharacterSelectParameters _CharacterNameType;
        public _EAllyType _AllyType;
        public bool _IsEquiped;//temp variable
        public bool _EnableHitReaction;
        public HealthSystem _HealthSystem;
        public Animator _Animator;
        public Rigidbody _RigidBody;
        public VicinityDetector _VicinityDetector;
        public Controller _Controller;
        public SubController _SubController;
        //public List<GameObject> _EnvironmentDetectedList = new List<GameObject>();
        public BoxCollider _BoxCollider;
        public CharacterRagdoll _CharacterRagdoll;
        public Vector3 _MoveDirection = new Vector3();
        public NpcBase _NPCBase;//basic npc setup
        public DamageDetector _DamageDetector;

        [Space(17)]
        [Header("Secondary Controller")]
        public bool _AttackTriggered;
        public bool _AttackButtonReset;
        public bool _Jump;

        private List<TriggerDetector> allTriggers = new List<TriggerDetector>();

        private void Awake()
        {
            _Animator = GetComponentInChildren<Animator>();
            _RigidBody = GetComponent<Rigidbody>();
            _Controller = GetComponent<Controller>();
            _SubController = GetComponent<SubController>();
            _BoxCollider = GetComponent<BoxCollider>();
            _CharacterRagdoll = GetComponent<CharacterRagdoll>();
            _CharacterRagdoll._BoxCollider = _BoxCollider;
            _CharacterRagdoll._Animator = _Animator; //get the animator from health class
            _CharacterRagdoll._RigidBody = _RigidBody;
            _CharacterRagdoll._Character = this;
            _VicinityDetector = GetComponentInChildren<VicinityDetector>();
            _NPCBase = GetComponentInChildren<NpcBase>();
            _DamageDetector = GetComponent<DamageDetector>();

            _HealthSystem = GetComponent<HealthSystem>();
            CharacterManager.GetInstance._AddCharacterList(this);
        }

        private void Update()
        {
            if (_Controller._Attack)
            {
                if (_AttackButtonReset)
                {
                    _AttackTriggered = true;
                    _AttackButtonReset = false;
                }
            }
            else
            {
                _AttackButtonReset = true;
            }

            if (Physics.Raycast(_BoxCollider.bounds.center, transform.forward, out RaycastHit hit, 8f)) // to become visible the health ui
            {
                Character c = hit.collider.transform.root.GetComponent<Character>();

                if (!_SubController.enabled)
                {
                    return;
                }

                if (null == c)
                {
                    return;
                }

                if (null == c._HealthSystem)
                {
                    return;
                }
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (_SubController.enabled)// guards for npc
            {
                return;
            }

            if (_CharacterRagdoll._RagdollParts.Contains(col))
            {
                return;
            }

            if (col.GetComponent<WeaponBase>())
            {
                return;
            }

            //check if ally or not
            Character target = col.transform.root.GetComponent<Character>();

            if (null == target)// nothing happens if detect the wall, ground or something
            {
                return;
            }

            if (null == _NPCBase)
            {
                return;
            }

            if (target._AllyType == _AllyType)
            {
                if (!_NPCBase.detectedAlly)
                {
                    _NPCBase.detectedAlly = true;
                }
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (_SubController.enabled)//guards for npc
            {
                return;
            }

            if (null != _NPCBase && _NPCBase.detectedAlly)
            {
                _NPCBase.detectedAlly = false;
            }
        }

        public List<TriggerDetector> _GetAllTriggers()
        {
            TriggerDetector[] triggers = GetComponentsInChildren<TriggerDetector>();

            foreach (TriggerDetector trigger in triggers)
            {
                if (!allTriggers.Contains(trigger))
                {
                    allTriggers.Add(trigger);
                }
            }

            return allTriggers;
        }

        public void _CharacterMove(Vector3 moveDirection, float movementSpeed, float speedGraph)// moving use by controller
        {
            transform.Translate(moveDirection.normalized * movementSpeed * speedGraph * Time.deltaTime, Space.World);
        }

        public void _CharacterMove(Vector3 moveDirection, float movementSpeed) //a bit move when its hit
        {
            transform.Translate(moveDirection.normalized * movementSpeed * Time.deltaTime, Space.World);
        }

        #region sphere edge initialize
        private void sphereEdge()
        {
            float top = _BoxCollider.bounds.center.y + _BoxCollider.bounds.extents.y;
            float bottom = _BoxCollider.bounds.center.y - _BoxCollider.bounds.extents.y;
            float front = _BoxCollider.bounds.center.z + _BoxCollider.bounds.extents.z;
            float back = _BoxCollider.bounds.center.z - _BoxCollider.bounds.extents.z;
            float left = _BoxCollider.bounds.center.x - _BoxCollider.bounds.extents.x;
            float right = _BoxCollider.bounds.center.x + _BoxCollider.bounds.extents.x;

            GameObject topFront = instantiateSphereEdgePrefab(new Vector3(0f, top, front));
            GameObject bottomFront = instantiateSphereEdgePrefab(new Vector3(0f, bottom, front));
            GameObject frontRight = instantiateSphereEdgePrefab(new Vector3(right, 0, front));
            GameObject frontLeft = instantiateSphereEdgePrefab(new Vector3(left, 0f, front));

            float xSection = (frontLeft.transform.position - frontRight.transform.position).magnitude / 6f;
            createSphereEdge(xSection, 5, frontLeft.transform.position, Vector3.right);

            float ySection = (bottomFront.transform.position - topFront.transform.position).magnitude / 9f;
            createSphereEdge(ySection, 8, bottomFront.transform.position, Vector3.up);
        }

        private void createSphereEdge(float sec, int interval, Vector3 startPosition, Vector3 direction)
        {
            for (int i = 0; i < interval; i++)
            {
                Vector3 position = startPosition + direction * (sec * (i + 1));
                instantiateSphereEdgePrefab(position);
            }
        }
        #endregion

        GameObject instantiateSphereEdgePrefab(Vector3 position)
        {
            return Instantiate(Resources.Load<GameObject>("SphereEdge"), position, Quaternion.identity, transform);
        }

        public void _CacheController(Animator anim)
        {
            foreach (BaseStateMachine o in anim.GetBehaviours<BaseStateMachine>())
            {
                o.controller = this;
            }
        }

        public Transform _BodyPartTransform(string bodyPartName)//need fix this line of code to get the exact tranform
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t.gameObject.name.Contains(bodyPartName))
                {
                    return t;
                }
            }

            return null;
        }

        //just to vanished dead body after a few seconds
        public IEnumerator _IEvanishDeadBody(float durationToVanish)
        {
            yield return new WaitForSeconds(durationToVanish);
            HealthSystem healthSystem = _HealthSystem;

            if (null != healthSystem._WeaponProjectileStuck) //just to leave the weapon stuck at the same localtion of the dead body
            {
                WeaponMelee weaponProc = healthSystem._WeaponProjectileStuck.GetComponent<WeaponMelee>();
                if (weaponProc._EnabledStucked)
                {
                    weaponProc.transform.parent = null;
                    weaponProc._RigidBody.isKinematic = false;
                    weaponProc._BoxCollider.isTrigger = false;
                    healthSystem._WeaponProjectileStuck = null;
                }
            }

            if (!_SubController.enabled)//temporary vanish dead body
            {
                Destroy(gameObject);//later we add some animation screen death for player
                CharacterManager.GetInstance._Characters.Remove(this);
            }
        }
    }
}


