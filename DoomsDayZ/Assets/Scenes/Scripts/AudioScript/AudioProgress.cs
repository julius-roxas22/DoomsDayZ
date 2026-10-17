using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;

namespace IndieGameAudioSystem
{
    public class AudioProgress : MonoBehaviour
    {
        public List<AudioData> _AudioDatas;

        public AudioClip _GetClip(_EAudioType audioType)
        {
            List<AudioClip> clips = new List<AudioClip>();

            foreach (AudioData data in _AudioDatas)
            {
                if (data._AudioType == audioType)
                {
                    clips = data._Clips;
                    break;
                }
            }

            if (clips.Count > 0)
            {
                return clips[Random.Range(0, clips.Count)];
            }
            return null;
        }
    }
}
