using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded
{
    [RequireComponent(typeof(Image))]
    public class DoImageColorOnMask : DoTweenOnMaskBase
    {
        [SerializeField] private Image _image;

        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Color _from = Color.white;
        [SerializeField] private Color _to = Color.white;

        [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        protected override Tween CreateTween()
        {
            return _image.DOColor(_to, _duration)
                .From(_from)
                .SetEase(_animationCurve);
        }

        private void Reset()
        {
            if (_image == null)
                _image = GetComponent<Image>();
        }
    }
}