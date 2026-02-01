using UnityEngine;

namespace Overcrowded.UI
{
    [CreateAssetMenu(menuName = "Overcrowded/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private string sceneName;
        [SerializeField] private string levelName;
        
        public string SceneName => sceneName;
        public string LevelName => levelName;
    }
}
