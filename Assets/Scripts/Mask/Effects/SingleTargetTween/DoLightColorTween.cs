using UnityEngine;

namespace Overcrowded.SingleTargetTween
{
    [RequireComponent(typeof(Light))]
    public class DoLightColorTweenOnMask : DoColorTweenOnMaskBase
    {
        [SerializeField] private Light _light;

        public override Color Current => _light.color;
        protected override void Set(Color value) => _light.color = value;

        private void Reset()
        {
            _light = GetComponent<Light>();
        }
    }
}