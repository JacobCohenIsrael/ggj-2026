using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Overcrowded.SingleTargetTween
{
    public abstract class DoColorTweenOnMaskBase : SingleTargetTweenOnMaskBase<Color>
    {
        [Serializable]
        public class MaskLight
        {
            [SerializeField] private Mask _mask;
            public Mask Mask => _mask;

            [SerializeField] private Color _color = Color.white;
            public Color Color => _color;
        }
        

        [SerializeField] private MaskLight[] _maskColors;
        [FormerlySerializedAs("_channelSpeed")] [SerializeField] private float _duration = 0.5f;

        protected override Color UpdateMoveTowards(Color targetValue)
        {
            if(_duration <= Mathf.Epsilon)
                return Target;

            var channelDelta = 1f / _duration * Time.deltaTime;
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
            var current = Current;
            return new(
                Mathf.MoveTowards(current.r, color.r, channelDelta),
                Mathf.MoveTowards(current.g, color.g, channelDelta),
                Mathf.MoveTowards(current.b, color.b, channelDelta)
            );
        }
    }
}