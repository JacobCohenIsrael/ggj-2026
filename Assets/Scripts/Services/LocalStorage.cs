using UnityEngine;

namespace Overcrowded.Services
{
    public class LocalStorage
    {
        private static class Constants
        {
            public const string MusicVolume = "MusicVolume";
            public const string SfxVolume = "SfxVolume";
        }
        
        private static float GetFloat(string key, float defaultValue)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        private static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }
        
        public float MusicVolume
        {
            get => GetFloat(Constants.MusicVolume, defaultValue: 1);
            set => SetFloat(Constants.MusicVolume, value);
        }

        public float SfxVolume
        {
            get => GetFloat(Constants.SfxVolume, defaultValue: 1);
            set => SetFloat(Constants.SfxVolume, value);
        }
    }
}