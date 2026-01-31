using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Overcrowded.Game.UI.MainMenu
{
    public class DarkOverlayController : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Fades")]
        [SerializeField] private FadeParams _death;
        public FadeParams Death => _death;

        [SerializeField] private FadeParams _levelStart;
        public FadeParams LevelStart => _levelStart;

        [SerializeField] private FadeParams _levelComplete;
        public FadeParams LevelComplete => _levelComplete;

        [SerializeField] private FadeParams _menuToLevel;
        public FadeParams MenuToLevel => _menuToLevel;

        [SerializeField] private FadeParams _settingsRestart;
        public FadeParams SettingsRestart => _settingsRestart;

        [SerializeField] private FadeParams _settingsToMenu;
        public FadeParams SettingsToMenu => _settingsToMenu;

        public bool FadedIn { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }

        public TweenerCore<float, float, FloatOptions> CreateFadeInTween(FadeParams fadeParams)
        {
            FadedIn = true;
            _canvasGroup.DOKill();
            _canvasGroup.blocksRaycasts = true;
            return _canvasGroup
                .DOFade(1f, fadeParams.Duration)
                .SetDelay(fadeParams.Delay)
                .SetEase(_curve);
        }

        public TweenerCore<float, float, FloatOptions> CreateFadeOutTween(FadeParams fadeParams)
        {
            FadedIn = false;
            _canvasGroup.DOKill();
            _canvasGroup.blocksRaycasts = false;
            return _canvasGroup
                .DOFade(0f, fadeParams.Duration)
                .From(1f)
                .SetDelay(fadeParams.Delay)
                .SetEase(_curve);
        }

        [Serializable]
        public class FadeParams
        {
            [SerializeField] private float _duration = 0.5f;
            public float Duration => _duration;

            [SerializeField] private float _delay = 0f;
            public float Delay => _delay;
        }
    }
}