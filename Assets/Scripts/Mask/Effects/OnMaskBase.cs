using System.Collections.Generic;
using JetBrains.Annotations;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public abstract class OnMaskBase : MonoBehaviour
    {
        [SerializeField] private List<Mask> _masks;
        [SerializeField] private bool _invert;
        [SerializeField] private bool _ignoreDelay;

        [Inject] private MaskChanger _maskChanger;

        [UsedImplicitly]
        public Mask Mask { get; private set; } = null;

        [UsedImplicitly]
        public bool Matches { get; private set; } = false;

        private bool _ignoringDelay;

        protected virtual void Awake()
        {
            _ignoringDelay = _ignoreDelay;

            _maskChanger.SubscribeMaskChanged(HandleMaskChanged, _ignoreDelay);

            var mask = _maskChanger.CurrentMask;
            var matches = IsMatching(mask);
            SetImmediate(mask, matches);
        }

        protected virtual void OnDestroy()
        {
            _maskChanger.UnsubscribeMaskChanged(HandleMaskChanged, _ignoreDelay);
        }

        public void AddMasks(params Mask[] masks)
        {
            _masks.AddRange(masks);

            HandleMaskChanged(_maskChanger.CurrentMask);
        }

        private void HandleMaskChanged(Mask newMask)
        {
            Mask = newMask;

            var matches = IsMatching(newMask);

            Matches = matches;

            OnMatchedChanged(newMask, Matches);
        }

        private bool IsMatching(Mask newMask)
        {
            var matches = _masks.Contains(newMask);

            if (_invert)
                matches = !matches;

            return matches;
        }

        protected abstract void OnMatchedChanged(Mask newMask, bool matches);
        protected abstract void SetImmediate(Mask mask, bool matches);
    }
}