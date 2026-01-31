using Overcrowded.MainMenu;
using UnityEngine;

namespace Overcrowded.UI
{
    public class LevelSelectView : MonoBehaviour
    {
        [SerializeField] private LevelConfig[] levelsConfig;
        [SerializeField] private SelectLevel selectLevelPrefab;
        [SerializeField] private Transform levelsContainer;
        
        private void Awake()
        {
            for (int i = 0; i < levelsConfig.Length; i++)
            {
                SelectLevel selectLevel = Instantiate(selectLevelPrefab, levelsContainer);
                selectLevel.Set(levelsConfig[i], i);
            }
        }
    }
}
