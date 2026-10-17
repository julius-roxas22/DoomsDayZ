using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameAudioSystem;
using IndieGameManagersSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Audio State Data/AudioOneShotState")]
    public class AudioOneShotState : BaseStateData
    {
        private AudioProgress progress;
        private AudioSource source;
        private bool inUpdateIsPlaying;

        [Header("Set up")]
        [SerializeField] private bool onStartOneShot;
        [SerializeField] private bool onUpdate;
        [SerializeField] private bool onExitOneShot;
        [SerializeField] private _EAudioType audioType;

        [Space(17)]
        [Header("Timing Impact")]
        [SerializeField] private float soundEffectTiming;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (null == progress)
            {
                progress = character.GetComponent<AudioProgress>();
            }

            if (onStartOneShot)
            {
                source = AudioManager.GetInstance._GetAudioSource(progress, audioType);
                source.PlayOneShot(progress._GetClip(audioType));
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (!onUpdate)
            {
                return;
            }

            if (inUpdateIsPlaying)
            {
                return;
            }

            if (stateInfo.normalizedTime >= soundEffectTiming)
            {
                source = AudioManager.GetInstance._GetAudioSource(progress, audioType);
                source.clip = progress._GetClip(audioType);
                source.Play();
                inUpdateIsPlaying = true;
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (onExitOneShot)
            {
                source = AudioManager.GetInstance._GetAudioSource(progress, audioType);
                source.PlayOneShot(progress._GetClip(audioType));
            }

            inUpdateIsPlaying = false;
        }


    }
}
