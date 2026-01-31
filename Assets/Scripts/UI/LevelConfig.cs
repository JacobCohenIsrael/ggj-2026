using UnityEditor;
using UnityEngine;

namespace Overcrowded.UI
{
    [CreateAssetMenu(menuName = "Overcrowded/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private SceneAsset sceneReference;
        [SerializeField] private string levelName;
    }
}
