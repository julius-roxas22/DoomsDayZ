using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAudioSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameManagersSystem
{
    public enum _EAudioType
    {
        NONE,
        THROW,
        SWING,
        PUNCH,
        WITH_BLADES,
        NO_BLADES,
        COLLIDE,
        FIST,
        FOOT_STEP,
        DIE,
        HURT,
        UPPERCUT,
        FIGHT_SCREAM,
        ROCK_TO_GROUND_IMPACT,
        CROSSPUNCH,
    }

    public class AudioManager : Singleton<AudioManager>
    {
        private Dictionary<AudioProgress, Dictionary<_EAudioType, AudioSource>> dictionary = new Dictionary<AudioProgress, Dictionary<_EAudioType, AudioSource>>();

        private void setup(AudioProgress progress)
        {
            if (!dictionary.TryGetValue(progress, out Dictionary<_EAudioType, AudioSource> source))
            {
                source = new Dictionary<_EAudioType, AudioSource>();
                dictionary[progress] = source;
            }
        }

        public AudioSource _GetAudioSource(AudioProgress progress, _EAudioType audioType)
        {
            setup(progress);

            Dictionary<_EAudioType, AudioSource> dictionarySource = dictionary[progress];

            if (!dictionarySource.TryGetValue(audioType, out AudioSource source))
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("SoundEffectFx"));
                source = obj.GetComponent<AudioSource>();
                dictionarySource[audioType] = source;
            }

            if (null != progress)
            {
                source.transform.position = progress.transform.position;
            }

            return source;
        }
    }
}
