using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;

namespace IndieGameAudioSystem
{
    [CreateAssetMenu(fileName = "New Audio Data", menuName = "IndieGamePractice/Audio Data/New Data")]

    public class AudioData : ScriptableObject
    {
        public _EAudioType _AudioType;
        public List<AudioClip> _Clips;
    }
}
