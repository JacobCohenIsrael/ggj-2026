using UnityEngine;

namespace Overcrowded
{
    public class UserState
    {
        private const string LevelKey = "Level";
        public int Level { get; private set; } = PlayerPrefs.GetInt(LevelKey, 1);

        public void IncreaseLevel()
        {
            Level++;
            PlayerPrefs.SetInt(LevelKey, Level);
        }
    }
}