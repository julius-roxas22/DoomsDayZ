using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameAudioSystem;
using IndieGameManagersSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Audio State Data/AudioMoveState")]
    public class AudioMoveState : BaseStateData
    {
        private AudioProgress progress;
        private AudioSource source;

        [Header("Set up")]
        [SerializeField] private float walkDelayTime;
        private float timerInterval;
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (null == progress)
            {
                progress = character.GetComponent<AudioProgress>();
            }
            timerInterval = 0f; //always start 0 to play the foot step effect for one step when it moves
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            //decrease everytime player is moving
            //applicable for npc and player with controller
            if (isGrounded(character) && (character._MoveDirection.sqrMagnitude > 0.01f || !character._SubController.enabled))
            {
                timerInterval -= Time.deltaTime;
                if (timerInterval <= 0f)
                {
                    playFootStep();
                    timerInterval = walkDelayTime;
                }
            }
            else
            {
                timerInterval = 0f;
            }

        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }


        private bool isGrounded(Character character)
        {
            if (Physics.Raycast(character._VicinityDetector.transform.position, -Vector3.up, 0.2f))
            {
                return true;
            }

            return false;
        }

        private void playFootStep()
        {
            source = AudioManager.GetInstance._GetAudioSource(progress, _EAudioType.FOOT_STEP);
            source.clip = progress._GetClip(_EAudioType.FOOT_STEP);
            source.Play();
        }

    }
}
