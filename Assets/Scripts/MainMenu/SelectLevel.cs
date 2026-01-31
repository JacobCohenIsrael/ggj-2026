using DG.Tweening;
using Overcrowded.Game.UI.MainMenu;
using Overcrowded.Services;
using Overcrowded.UI;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded.MainMenu
{
    public class SelectLevel : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text levelNumberText;
        [SerializeField] private GameObject locked;
        [SerializeField] private string lockedText;
        
        [Inject] private UserState userState;
        [Inject] private DarkOverlayController darkOverlay;
        [Inject] private LevelLoader levelLoader;
        
        private LevelConfig levelConfig;
        private int level;
        private bool isLocked;

        public void Set(LevelConfig config, int levelIndex)
        {
            levelConfig = config;
            level = levelIndex + 1;
            isLocked = level > userState.Level;
            locked.SetActive(isLocked);
            
            var levelName = isLocked ? lockedText : levelConfig.LevelName;
            levelNumberText.text = $"{level} {levelName}";
        }

        private void OnEnable()
        {
            button.onClick.AddListener(LoadLevelScene);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(LoadLevelScene);
        }

        private void LoadLevelScene()
        {
            if (isLocked)
                return;

            levelLoader.LoadLevel(levelConfig.SceneReference.name, darkOverlay.MenuToLevel);
        }
    }
}