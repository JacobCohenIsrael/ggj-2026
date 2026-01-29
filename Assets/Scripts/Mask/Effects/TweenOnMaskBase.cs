using UnityEngine;

namespace Overcrowded
{
    public abstract class TweenOnMaskBase : OnMaskBase
    {
        protected abstract float Duration { get; }

        private float _progress = 0f;

        private void Update()
        {
            var targetProgress = Matches ? 1f : 0f;

            if (Mathf.Approximately(Duration, 0f))
            {
                Set(targetProgress);
                return;
            }

            var newProgress = Mathf.MoveTowards(_progress, targetProgress, Time.deltaTime / Duration);

            if(Mathf.Approximately(newProgress, _progress))
                return;

            _progress = newProgress;

            Set(_progress);
        }

        protected abstract void Set(float progress);

        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
        }
    }
}