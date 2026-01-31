using System;
using UnityEngine;

namespace Overcrowded.Services
{
    public class UserState
    {
        private const string LevelKey = "Level";
        public int Level { get; private set; } = PlayerPrefs.GetInt(LevelKey, 1);

        public void SetLevelCompleted(int levelIndex)
        {
            Level = Math.Max(Level, levelIndex + 1);
            PlayerPrefs.SetInt(LevelKey, Level);
        }
    }
}