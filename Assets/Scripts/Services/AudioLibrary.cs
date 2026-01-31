using UnityEngine;

namespace Overcrowded.Services
{
    [CreateAssetMenu(menuName = "Overcrowded/AudioLibrary", fileName = "AudioLibrary")]
    public class AudioLibrary : ScriptableObject
    {
        public AudioClip LevelMusicClip;
        public AudioClip MaskChangeClip;
    }
}