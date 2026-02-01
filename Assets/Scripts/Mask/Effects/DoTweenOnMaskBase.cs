using DG.Tweening;

namespace Overcrowded
{
    public abstract class DoTweenOnMaskBase : OnMaskBase
    {
        private Tween _tween;

        private bool _lastMatches;

        protected abstract Tween CreateTween();

        protected override void Awake()
        {
            base.Awake();

            _tween = CreateTween();
            _tween
                .SetAutoKill(false)
                .Pause();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _tween.Kill();
        }

        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
            if (_lastMatches == matches)
                return;

            _lastMatches = matches;

            if (matches)
                _tween.PlayForward();
            else
                _tween.PlayBackwards();
        }

        protected override void SetImmediate(Mask mask, bool matches)
        {
            if (matches)
                _tween.Goto(_tween.Duration(), andPlay: false);
            else
                _tween.Goto(0, andPlay: false);
        }
    }
}