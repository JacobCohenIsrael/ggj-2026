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

            UpdateProgress(newProgress);
        }

        private void UpdateProgress(float progress)
        {
            if(Mathf.Approximately(progress, _progress))
                return;

            _progress = progress;
            Set(_progress);
        }

        protected override void SetImmediate(Mask mask, bool matches)
        {
            _progress = matches ? 1f : 0f;
            UpdateProgress(_progress);
        }

        protected abstract void Set(float progress);

        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
        }
    }
}