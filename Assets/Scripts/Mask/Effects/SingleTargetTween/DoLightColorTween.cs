using System;
using UnityEngine;

namespace Overcrowded.SingleTargetTween
{
    [RequireComponent(typeof(Light))]
    public class DoLightColorTweenOnMask : SingleTargetOnMaskBase
    {
        [Serializable]
        public class MaskLight
        {
            [SerializeField] private Mask _mask;
            public Mask Mask => _mask;

            [SerializeField] private Color _color = Color.white;
            public Color Color => _color;
        }

        [SerializeField] private Light _light;
        [SerializeField] private MaskLight[] _maskColors;
        [SerializeField] private float _channelSpeed = 1f;

        public Color Color => _light.color;

        private Color _originalColor = Color.black;

        private Color _targetColor;

        protected override void Awake()
        {
            base.Awake();

            _originalColor = _light.color;
            _targetColor = _light.color;
        }

        private void Update()
        {
            if(_channelSpeed <= Mathf.Epsilon)
                return;

            var channelDelta = 1f / _channelSpeed * Time.deltaTime;
            _light.color = MoveTowards(_targetColor, channelDelta);
        }

        private Color MoveTowards(Color color, float channelDelta)
        {
            return new(
                Mathf.MoveTowards(_light.color.r, color.r, channelDelta),
                Mathf.MoveTowards(_light.color.g, color.g, channelDelta),
                Mathf.MoveTowards(_light.color.b, color.b, channelDelta)
            );
        }

        private Color GetColorForMask(Mask mask)
        {
            foreach (var maskColor in _maskColors)
            {
                if (maskColor.Mask == mask)
                    return maskColor.Color;
            }

            return _originalColor;
        }


        protected override void HandleMaskChanged(Mask mask)
        {
            _targetColor = GetColorForMask(mask);
        }

        protected override void SetImmediate(Mask mask)
        {
            _light.color = _targetColor = GetColorForMask(mask);
        }
    }
}