using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameUISystem;

//by using base script; it is shared with player and npc
//this is for weapon interaction, such as throwing
namespace IndieGameInventorySystem
{
    //im just added some default values for the reusable variable
    public abstract class WeaponBase : MonoBehaviour
    {
        //need to poolish the sound effect for every weapon such as heavy or melee.

        //shared this in subclasses
        protected bool isCollided;

        [Header("Basic setup")]
        [SerializeField] protected Vector3 rotateDirection = Vector3.forward * 700f; //direction to rotate the object
        [SerializeField] protected Vector3 piercePosition = new Vector3(0f, 0.15f, 0.15f); //position of the object when its stucked
        [SerializeField] protected List<string> ignoreCollisions = new List<string>();//temporary declare here; entries of object can ignore collisions
        [SerializeField] protected float travelSpeed = 6f;//move speed of this object
        [SerializeField] protected float maxDistance = 17f;//maximum distance 

        private Light pointLight;
        private ItemCanvasContainer weaponCanvasContainer;

        //temporary visible variable
        [Space(17)]
        [Header("Weapon Details")] //temporary visible variable
        public Character _Attacker;//who is the attacker of this object
        public Vector3 _MoveDirection;//where is going 
        public Rigidbody _RigidBody;
        public bool _EnabledStucked; //being stucked into which body or object has been hit
        public bool _IsThrowing;

        [Space(17)]
        [Header("Damage impact")]
        public float _XForce;
        public float _YForce = 200f;
        public float _ZForce = 1300f;

        public abstract void Awake(); //abstract function will implement into sub classes

        public abstract void Update();

        public abstract void OnTriggerEnter(Collider col);

        public Light _Light
        {
            get
            {
                if (null == pointLight)
                {
                    pointLight = GetComponentInChildren<Light>();
                }
                return pointLight;
            }
        }

        public ItemCanvasContainer _WeaponContainer
        {
            get
            {
                if (null == weaponCanvasContainer)
                {
                    weaponCanvasContainer = GetComponentInChildren<ItemCanvasContainer>();
                }
                return weaponCanvasContainer;
            }
        }

        public static bool _IsWeaponProjectile(Collider col)//can walk when character passes by
        {
            WeaponMelee proc = col.transform.root.GetComponent<WeaponMelee>();

            if (null != proc)
            {
                return true;
            }

            return false;
        }

    }
}