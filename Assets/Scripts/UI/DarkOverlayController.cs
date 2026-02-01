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

        [SerializeField] private CanvasGroupFadeParams _goodJob;
        [SerializeField] private CanvasGroupFadeParams _youGainedAMask;

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

        public TweenerCore<float, float, FloatOptions> CreateFadeInTween(FadeParams fadeParams, AnimationCurve curve = null)
        {
            curve ??= fadeParams.OverrideCurve ? fadeParams.Curve : _curve;

            FadedIn = true;
            _canvasGroup.DOKill();
            _canvasGroup.blocksRaycasts = true;
            return _canvasGroup
                .DOFade(1f, fadeParams.Duration)
                .SetDelay(fadeParams.Delay)
                .SetEase(curve);
        }

        public TweenerCore<float, float, FloatOptions> CreateFadeOutTween(FadeParams fadeParams, AnimationCurve curve = null)
        {
            curve ??= fadeParams.OverrideCurve ? fadeParams.Curve : _curve;

            FadedIn = false;
            _canvasGroup.DOKill();
            _canvasGroup.blocksRaycasts = false;
            return _canvasGroup
                .DOFade(0f, fadeParams.Duration)
                .From(1f)
                .SetDelay(fadeParams.Delay)
                .SetEase(curve);
        }

        public void CreateGoodJobTween()
        {
            TweenCanvasGroup(_goodJob);
        }

        public void CreatedYouGainedAMaskTween()
        {
            TweenCanvasGroup(_youGainedAMask);
        }

        private void TweenCanvasGroup(CanvasGroupFadeParams fadeParams)
        {
            fadeParams.CanvasGroup.DOKill();
            fadeParams.CanvasGroup.alpha = 0f;

            var tweenDuration = (fadeParams.Duration - fadeParams.HoldDuration) / 2f;

            var fadeIn = fadeParams.CanvasGroup
                .DOFade(1f, tweenDuration)
                .SetEase(fadeParams.Curve)
                .SetUpdate(true)
                .SetDelay(fadeParams.Delay);

            var fadeOut = fadeParams.CanvasGroup
                .DOFade(0f, tweenDuration)
                .SetEase(fadeParams.Curve)
                .SetUpdate(true)
                .SetDelay(fadeParams.HoldDuration);

            var sequence = DOTween.Sequence();
            sequence.Append(fadeIn);
            sequence.Append(fadeOut);
            sequence.Play();
        }

        [Serializable]
        public class CanvasGroupFadeParams
        {
            [SerializeField] private CanvasGroup _canvasGroup;
            public CanvasGroup CanvasGroup => _canvasGroup;

            [SerializeField] private float _duration = 1f;
            public float Duration => _duration;

            [SerializeField] private float _holdDuration = 0f;
            public float HoldDuration => _holdDuration;

            [SerializeField] private float _delay = 0f;
            public float Delay => _delay;

            [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
            public AnimationCurve Curve => _curve;
        }

        [Serializable]
        public class FadeParams
        {
            [SerializeField] private bool _overrideCurve = false;
            public bool OverrideCurve => _overrideCurve;

            [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);
            public AnimationCurve Curve => _curve;

            [SerializeField] private float _duration = 0.5f;
            public float Duration => _duration;

            [SerializeField] private float _delay = 0f;
            public float Delay => _delay;
        }
    }
}