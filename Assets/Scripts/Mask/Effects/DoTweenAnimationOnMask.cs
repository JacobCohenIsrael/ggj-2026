using DG.Tweening;
using UnityEngine;

namespace Overcrowded
{
    [RequireComponent(typeof(DOTweenAnimation))]
    public class DoTweenAnimationOnMask : DoTweenOnMaskBase
    {
        [SerializeField] private DOTweenAnimation _tween;

        protected override Tween CreateTween()
        {
            return _tween.tween;
        }

        private void Reset()
        {
            _tween = GetComponent<DOTweenAnimation>();
        }

        private void OnValidate()
        {
            if (_tween == null)
                _tween = GetComponent<DOTweenAnimation>();

            _tween.autoKill = false;
            _tween.autoPlay = false;
        }
    }
}