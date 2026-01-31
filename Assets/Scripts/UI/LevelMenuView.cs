using DG.Tweening;
using JetBrains.Annotations;
using Overcrowded.Game.UI.MainMenu;
using Overcrowded.Services;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded
{
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private float _moveDistance = 1000f;
        [SerializeField] private Transform _settingsButtonsParent;
        [SerializeField] private Button _closeSettingsButton;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Image _settingsBackground;
        [SerializeField] private float _settingsBackgroundAlpha = 0.5f;

        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        [Inject] private LevelLoader _levelLoader;
        [Inject] private DarkOverlayController _darkOverlay;

        public bool SettingsShown { get; private set; }

        private void Awake()
        {
            SetPositionX(_settingsButtonsParent.transform, _moveDistance);

            _closeSettingsButton.onClick.AddListener(CloseSettings);

            _restartButton.onClick.AddListener(RestartLevel);
            _mainMenuButton.onClick.AddListener(GoToMainMenu);
        }

        private void GoToMainMenu()
        {
            _levelLoader.LoadLevel("MainMenu", _darkOverlay.SettingsToMenu);

            CloseSettings();
        }

        private void RestartLevel()
        {
            _levelLoader.LoadLevel(gameObject.scene.name, _darkOverlay.SettingsRestart);

            CloseSettings();
        }

        public void ToggleSettings()
        {
            if (SettingsShown)
                CloseSettings();
            else
                OpenSettings();
        }

        [UsedImplicitly]
        public void OpenSettings()
        {
            _settingsBackground.DOKill();
            _settingsBackground
                .DOFade(_settingsBackgroundAlpha, _duration)
                .SetUpdate(true);

            _settingsButtonsParent.DOKill();
            _settingsButtonsParent
                .DOLocalMoveX(0f, _duration)
                .SetUpdate(true);

            SettingsShown = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }

        private void CloseSettings()
        {
            _settingsBackground.DOKill();
            _settingsBackground
                .DOFade(0f, _duration)
                .SetUpdate(true);

            _settingsButtonsParent.DOKill();
            _settingsButtonsParent
                .DOLocalMoveX(_moveDistance, _duration)
                .SetUpdate(true);

            SettingsShown = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }

        private static void SetPositionX(Transform target, float x)
        {
            var position = target.localPosition;
            position.x = x;
            target.localPosition = position;
        }
    }
}