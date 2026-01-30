using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded.Game.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private Button _closeSettingsButton;

        [SerializeField] private Button _openLevelsButton;
        [SerializeField] private Button _closeLevelsButton;

        [Header("Animation")] [SerializeField] private float _moveDistance = 500f;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Transform _mainMenuButtonsParent;
        [SerializeField] private Transform _settingsButtonsParent;
        [SerializeField] private Transform _levelSelectionButtonsParent;

        private void Awake()
        {
            SetPositionX(_settingsButtonsParent, _moveDistance);
            SetPositionX(_levelSelectionButtonsParent, _moveDistance);

            _openSettingsButton.onClick.AddListener(OpenSettings);
            _closeSettingsButton.onClick.AddListener(CloseSettings);

            _openLevelsButton.onClick.AddListener(OpenLevelSelection);
            _closeLevelsButton.onClick.AddListener(CloseLevelSelection);
        }

        private static void SetPositionX(Transform target, float x)
        {
            var position = target.localPosition;
            position.x = x;
            target.localPosition = position;
        }

        private void OpenSettings()
        {
            _mainMenuButtonsParent.DOKill();
            _mainMenuButtonsParent.DOScale(0f, _duration);

            _settingsButtonsParent.DOKill();
            _settingsButtonsParent.DOLocalMoveX(0f, _duration);
        }

        private void CloseSettings()
        {
            _mainMenuButtonsParent.DOKill();
            _mainMenuButtonsParent.DOScale(1f, _duration);

            _settingsButtonsParent.DOKill();
            _settingsButtonsParent.DOLocalMoveX(_moveDistance, _duration);
        }

        private void OpenLevelSelection()
        {
            _mainMenuButtonsParent.DOKill();
            _mainMenuButtonsParent.DOScale(0f, _duration);

            _levelSelectionButtonsParent.DOKill();
            _levelSelectionButtonsParent.DOLocalMoveX(0f, _duration);
        }

        private void CloseLevelSelection()
        {
            _mainMenuButtonsParent.DOKill();
            _mainMenuButtonsParent.DOScale(1f, _duration);

            _levelSelectionButtonsParent.DOKill();
            _levelSelectionButtonsParent.DOLocalMoveX(_moveDistance, _duration);
        }
    }
}