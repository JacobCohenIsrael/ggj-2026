using Overcrowded.Animations;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public abstract class FadeOutOnMaskBase : TweenOnMaskBase
    {
        [Inject] private AnimationConfigs _animationConfigs;

        private float? _alpha;

        protected override float Duration => _animationConfigs.FadeOutParams.Duration;

        protected override void Set(float progress)
        {
            var curve = _animationConfigs.FadeOutParams.AnimationCurve;
            var evaluatedProgress = curve.Evaluate(progress);
            var newAlpha = Mathf.Lerp(1f, 0f, evaluatedProgress);

            if (_alpha.HasValue && Mathf.Approximately(newAlpha, _alpha.Value))
                return;

            _alpha = newAlpha;

            SetAlpha(newAlpha);
        }

        protected abstract void SetAlpha(float alpha);
    }
}