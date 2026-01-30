using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded.Game.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private CreditsView _creditsView;

        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private Button _closeSettingsButton;

        [SerializeField] private Button _openLevelsButton;
        [SerializeField] private Button _closeLevelsButton;

        [SerializeField] private Button _openCreditsButton;
        [SerializeField] private Button _closeCreditsButton;

        [SerializeField] private Button _quitButton;

        [Header("Animation")] [SerializeField] private float _moveDistance = 500f;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Transform _mainMenuButtonsParent;
        [SerializeField] private Transform _settingsButtonsParent;
        [SerializeField] private Transform _levelSelectionButtonsParent;

        private bool _settingsOpen;
        private bool _levelSelectionOpen;
        private bool _creditsOpen;

        private void Awake()
        {
            SetPositionX(_settingsButtonsParent, _moveDistance);
            SetPositionX(_levelSelectionButtonsParent, _moveDistance);

            _openSettingsButton.onClick.AddListener(OpenSettings);
            _closeSettingsButton.onClick.AddListener(CloseSettings);

            _openLevelsButton.onClick.AddListener(OpenLevelSelection);
            _closeLevelsButton.onClick.AddListener(CloseLevelSelection);

            _openCreditsButton.onClick.AddListener(OpenCredits);
            _closeCreditsButton.onClick.AddListener(CloseCredits);

            if (Application.platform == RuntimePlatform.WebGLPlayer)
                _quitButton.gameObject.SetActive(false);
            else
                _quitButton.onClick.AddListener(Application.Quit);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
                return;

            if(_settingsOpen)
                CloseSettings();

            else if(_levelSelectionOpen)
                CloseLevelSelection();

            else if(_creditsOpen)
                CloseCredits();
        }

        private static void SetPositionX(Transform target, float x)
        {
            var position = target.localPosition;
            position.x = x;
            target.localPosition = position;
        }

        private void OpenSettings()
        {
            if(_settingsOpen)
                return;
            _settingsOpen = true;

            _mainMenuButtonsParent.DOKill();
            _mainMenuButtonsParent.DOScale(0f, _duration);

            _settingsButtonsParent.DOKill();
            _settingsButtonsParent.DOLocalMoveX(0f, _duration);
        }

        private void CloseSettings()
        {
            if(!_settingsOpen)
                return;
            _settingsOpen = false;

            _mainMenuButtonsParent.DOKill();
            _mainMenuButtonsParent.DOScale(1f, _duration);

            _settingsButtonsParent.DOKill();
            _settingsButtonsParent.DOLocalMoveX(_moveDistance, _duration);
        }

        private void OpenLevelSelection()
        {
            if(_levelSelectionOpen)
                return;
            _levelSelectionOpen = true;

            _mainMenuButtonsParent.DOKill();
            _mainMenuButtonsParent.DOScale(0f, _duration);

            _levelSelectionButtonsParent.DOKill();
            _levelSelectionButtonsParent.DOLocalMoveX(0f, _duration);
        }

        private void CloseLevelSelection()
        {
            if(!_levelSelectionOpen)
                return;
            _levelSelectionOpen = false;

            _mainMenuButtonsParent.DOKill();
            _mainMenuButtonsParent.DOScale(1f, _duration);

            _levelSelectionButtonsParent.DOKill();
            _levelSelectionButtonsParent.DOLocalMoveX(_moveDistance, _duration);
        }

        private void OpenCredits()
        {
            if(_creditsOpen)
                return;
            _creditsOpen = true;

            _creditsView.CreateCreditsTween();
        }

        private void CloseCredits()
        {
            if (!_creditsOpen)
                return;
            _creditsOpen = false;

            _creditsView.CreateCloseTween();
        }
    }
}