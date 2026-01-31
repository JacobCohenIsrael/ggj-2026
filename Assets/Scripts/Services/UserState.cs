using System;
using UnityEngine;

namespace Overcrowded
{
    public class UserState
    {
        private const string LevelKey = "Level";
        public int Level { get; private set; } = PlayerPrefs.GetInt(LevelKey, 1);

        public void SetLevelCompleted(int level)
        {
            Level = Math.Max(Level, level + 1);
            PlayerPrefs.SetInt(LevelKey, Level);
        }
    }
}