using System;
using UnityEngine;

namespace Overcrowded.SingleTargetTween
{
    [RequireComponent(typeof(Light))]
    public class DoLightColorTweenOnMask : SingleTargetTweenOnMaskBase<Color>
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

        public override Color Current => _light.color;

        protected override void Set(Color value)
        {
            _light.color = value;
        }

        protected override Color UpdateMoveTowards(Color targetValue)
        {
            if(_channelSpeed <= Mathf.Epsilon)
                return Target;

            var channelDelta = 1f / _channelSpeed * Time.deltaTime;
            return MoveTowards(targetValue, channelDelta);
        }

        protected override Color GetTargetForMask(Mask mask)
        {
            foreach (var maskColor in _maskColors)
            {
                if (maskColor.Mask == mask)
                    return maskColor.Color;
            }

            return Initial;
        }

        private Color MoveTowards(Color color, float channelDelta)
        {
            return new(
                Mathf.MoveTowards(_light.color.r, color.r, channelDelta),
                Mathf.MoveTowards(_light.color.g, color.g, channelDelta),
                Mathf.MoveTowards(_light.color.b, color.b, channelDelta)
            );
        }
    }
}