using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameAudioSystem;
using IndieGameUISystem;
using TMPro;

namespace IndieGameAttackSystem
{
    public class DamageDetector : MonoBehaviour
    {
        private Character character;
        private AudioSource attackerSource; //attacker when it collide his attack
        private AudioSource hurtSource;
        private bool attackerSoundPlayed;
        private bool hurtSoundPlayed;

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        void Update()
        {
            if (AttackManager.GetInstance._CurrentAttacks.Count > 0)
            {
                foreach (AttackInfo info in AttackManager.GetInstance._CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (!info._IsRegisteredAttack)
                    {
                        continue;
                    }

                    if (info._Attacker == character)
                    {
                        continue;
                    }

                    if (info._Attacker._AllyType == character._AllyType)
                    {
                        continue;
                    }

                    if (info._MustCollide)
                    {
                        if (checkAttack(info))
                        {
                            takeDamage(info);
                            attackerSoundEffect(info._Attacker);
                            hurtSoundEffect();//temporary hurt sound
                        }
                        attackerSoundPlayed = false;
                        hurtSoundPlayed = false;
                    }
                }
            }
        }

        private void attackerSoundEffect(Character attacker)
        {
            if (!attacker._SubController.enabled)//not applicable for npc
            {
                return;
            }

            if (attackerSoundPlayed)
            {
                return;
            }

            attackerSoundPlayed = true;
            AudioProgress progress = null;
            if (attacker._IsEquiped)
            {
                List<GameObject> list = WeaponManager.GetInstance._GetWeaponList(attacker);
                progress = list[0].GetComponent<AudioProgress>(); //get the weapon progress property
                _EAudioType audioType = _EAudioType.NONE;
                foreach (AudioData data in progress._AudioDatas)
                {
                    if (data._AudioType == _EAudioType.WITH_BLADES || data._AudioType == _EAudioType.NO_BLADES)
                    {
                        audioType = data._AudioType;
                    }
                }
                attackerSource = AudioManager.GetInstance._GetAudioSource(progress, audioType);
                attackerSource.clip = progress._GetClip(audioType);
            }
            else
            {
                progress = attacker.GetComponent<AudioProgress>();
                attackerSource = AudioManager.GetInstance._GetAudioSource(progress, _EAudioType.FIST);
                attackerSource.clip = progress._GetClip(_EAudioType.FIST);
            }

            attackerSource.Play();
        }

        private bool checkAttack(AttackInfo info)
        {
            foreach (TriggerDetector trigger in character._GetAllTriggers())
            {
                foreach (Collider col in trigger._CollidingParts)
                {
                    if (null == col)
                    {
                        continue;
                    }

                    foreach (string part in info._AttackParts) //need to change this line of code
                    {
                        if (col.name.Equals(part))
                        {
                            character._CharacterRagdoll._Attacker = info._Attacker;
                            character._CharacterRagdoll._DirectionDamageImpact = new Vector3(info._BaseDamageType._XForce, info._BaseDamageType._YForce, info._BaseDamageType._ZForce);
                            character._CharacterRagdoll._TriggerPart = trigger;
                            character._CharacterRagdoll._IsBodyPartHit = true;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //this work only in melee combat
        //i want to enable the pure ragdoll death only once
        private void takeDamage(AttackInfo info)
        {
            CameraManager.GetInstance._TriggerShakeCamera(0.55f);

            if (!info._BaseDamageType._AttackersAttack.Add(info._Attacker))//prevent consecutive damage when its collide
            {
                return;
            }

            character._HealthSystem._DefenseData._CurrentHealth -= info._Damage;
            _HitReactionTriggered(character);
            _ShowDamageText(info._Damage);
            character._CharacterRagdoll._RagdollChecker(character);
        }

        //boomer explode damage
        public void _TakeDamage(Character damageTaker, int damage)
        {
            //just simply decrease the current health
            //check if can turn on the ragdoll wizzard
            //check if there is a hit reaction activated
            //shake the camera within range
            CameraManager.GetInstance._TriggerShakeCamera(0.9f);
            damageTaker._HealthSystem._DefenseData._CurrentHealth -= damage;
            _HitReactionTriggered(character);
            _ShowDamageText(damage);
            character._CharacterRagdoll._RagdollChecker(damageTaker);
        }

        //weapon heavy and throwable melee weapon damage
        public void _TakeDamage(Vector3 damageImpactDirection, Character attacker, Collider collidedObject, int damage)
        {
            Character damageTaker = collidedObject.transform.root.GetComponent<Character>();
            Collider col = damageTaker._CharacterRagdoll._GetCollidedPart(collidedObject);

            if (null != damageTaker._CharacterRagdoll)
            {
                damageTaker._CharacterRagdoll._Attacker = attacker;
                damageTaker._CharacterRagdoll._DirectionDamageImpact = damageImpactDirection;
                damageTaker._CharacterRagdoll._TriggerPart = col.GetComponent<TriggerDetector>();
            }

            if (null != damageTaker._DamageDetector)
            {
                IndieGameHealthSystem.HealthSystem healthSystem = damageTaker._HealthSystem;
                healthSystem._DefenseData._CurrentHealth -= damage;
                _ShowDamageText(damage);
                character._CharacterRagdoll._RagdollChecker(damageTaker);
            }

            _HitReactionTriggered(damageTaker);//hit reaction when it take damage
        }

        private void hurtSoundEffect()
        {
            //damage taker sound effect
            if (hurtSoundPlayed)
            {
                return;
            }

            hurtSoundPlayed = true;
            AudioProgress progress = character.GetComponent<AudioProgress>();
            hurtSource = AudioManager.GetInstance._GetAudioSource(progress, _EAudioType.HURT);
            AudioClip clip = progress._GetClip(_EAudioType.HURT);

            if (null == clip)
            {
                return;
            }

            hurtSource.clip = clip;
            hurtSource.Play();
        }

        private void _ShowDamageText(int damage)//it show`s the damage of the attacker
        {
            //GameObject gameObject
            GameObject obj = GameManager.GetInstance._GetGameUI()._GetDamageTextRootParent();
            CharacterCanvasContainer characterCanvasContainer = character.GetComponentInChildren<CharacterCanvasContainer>();
            obj.transform.parent = characterCanvasContainer.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            characterCanvasContainer._DamageTakenText = obj.GetComponentInChildren<TMP_Text>();
            characterCanvasContainer._DamageTakenText.SetText(damage.ToString());
        }

        public void _HitReactionTriggered(Character character)
        {
            if (!character._EnableHitReaction) // add guards here to prevent hit reaction
            {
                return;
            }

            if (character._IsEquiped /*|| WeaponManager.GetInstance._GetWeaponList(character).Count > 0*/)
            {
                character._Animator.runtimeAnimatorController = character._HealthSystem._HitReactionList[1];//back to weapon with idle reaction
            }
            else
            {
                character._Animator.runtimeAnimatorController = character._HealthSystem._HitReactionList[0];//back to normal hit reaction
            }
        }
    }
}

