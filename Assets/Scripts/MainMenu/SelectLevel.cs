using DG.Tweening;
using Overcrowded.Game.UI.MainMenu;
using Overcrowded.Services;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded
{
    public class SelectLevel : MonoBehaviour
    {
        [SerializeField] private int levelNumber;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text levelNumberText;
        [SerializeField] private GameObject locked;
        [SerializeField] private string _buttonPrefix = "Level ";

        [Inject] private UserState userState;
        [Inject] private DarkOverlayController _darkOverlay;
        [Inject] private LevelLoader _levelLoader;

        private const string LevelSceneName = "Level_";

        public bool Locked => levelNumber > userState.Level;

        private void Awake()
        {
            locked.SetActive(Locked);
            
            levelNumberText.text = $"{_buttonPrefix} {levelNumber}";
            button.onClick.AddListener(LoadLevelScene);
        }

        private void LoadLevelScene()
        {
            if (Locked)
                return;

            _darkOverlay.CreateFadeInTween(_darkOverlay.MenuToLevel)
                .OnComplete(LoadScene);

        }

        private void LoadScene()
        {
            _levelLoader.LoadLevel($"{LevelSceneName}{levelNumber}");
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(LoadLevelScene);
        }
    }
}