using JetBrains.Annotations;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded.SingleTargetTween
{
    public abstract class SingleTargetOnMaskBase : MonoBehaviour
    {
        [Inject] private MaskChanger _maskChanger;

        [UsedImplicitly]
        public Mask Mask { get; private set; } = null;

        protected virtual void Awake()
        {
            _maskChanger.OnMaskChanged += HandleMaskChanged;

            var mask = _maskChanger.CurrentMask;
            SetImmediate(mask);
        }

        protected virtual void OnDestroy()
        {
            _maskChanger.OnMaskChanged -= HandleMaskChanged;
        }

        protected abstract void HandleMaskChanged(Mask mask);

        protected abstract void SetImmediate(Mask mask);
    }
}