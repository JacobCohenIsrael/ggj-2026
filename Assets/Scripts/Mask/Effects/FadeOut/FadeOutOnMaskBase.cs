using Overcrowded.Animations;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public abstract class FadeOutOnMaskBase : TweenOnMaskBase
    {
        [Inject] private MaskEffectsVisualConfigs _maskEffectsVisualConfigs;

        private float? _alpha;

        protected override float Duration => _maskEffectsVisualConfigs.FadeOutParams.Duration;

        protected override void Set(float progress)
        {
            var curve = _maskEffectsVisualConfigs.FadeOutParams.AnimationCurve;
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