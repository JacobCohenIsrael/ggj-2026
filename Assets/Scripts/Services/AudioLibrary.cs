using System;
using UnityEngine;

namespace Overcrowded.Services
{
    [CreateAssetMenu(menuName = "Overcrowded/AudioLibrary", fileName = "AudioLibrary")]
    public class AudioLibrary : ScriptableObject
    {
        public AudioClipRecord LevelMusicClipRecord;
        public AudioClipRecord MaskChangeClipRecord;
        public AudioClipRecord SlideEffectClipRecord;
    }

    [Serializable]
    public class AudioClipRecord
    {
        public AudioClip Clip;
        
        [Range(0f, 1f)]
        public float Volume;
    }
    
}