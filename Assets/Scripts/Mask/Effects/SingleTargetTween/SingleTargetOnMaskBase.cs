using JetBrains.Annotations;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded.SingleTargetTween
{
    public abstract class SingleTargetOnMaskBase : MonoBehaviour
    {
        [SerializeField] private bool _ignoreDelay;

        [Inject] private MaskChanger _maskChanger;

        private bool _ignoringDelay;

        [UsedImplicitly]
        public Mask Mask { get; private set; } = null;

        protected virtual void Awake()
        {
            _ignoringDelay = _ignoreDelay;

            _maskChanger.SubscribeMaskChanged(HandleMaskChanged, _ignoringDelay);

            var mask = _maskChanger.CurrentMask;
            SetImmediate(mask);
        }

        protected virtual void OnDestroy()
        {
            _maskChanger.UnsubscribeMaskChanged(HandleMaskChanged, _ignoringDelay);
        }

        protected abstract void HandleMaskChanged(Mask mask);

        protected abstract void SetImmediate(Mask mask);
    }
}