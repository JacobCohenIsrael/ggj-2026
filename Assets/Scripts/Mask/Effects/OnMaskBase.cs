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

        [Inject] private MaskChanger _maskChanger;

        [UsedImplicitly]
        public Mask Mask { get; private set; } = null;

        [UsedImplicitly]
        public bool Matches { get; private set; } = false;


        protected virtual void Awake()
        {
            _maskChanger.OnMaskChanged += HandleMaskChanged;
        }

        protected virtual void OnDestroy()
        {
            _maskChanger.OnMaskChanged -= HandleMaskChanged;
        }

        public void AddMasks(params Mask[] masks)
        {
            _masks.AddRange(masks);

            HandleMaskChanged(_maskChanger.CurrentMask);
        }

        private void HandleMaskChanged(Mask newMask)
        {
            Mask = newMask;

            var matches = _masks.Contains(newMask);

            if (_invert)
                matches = !matches;

            Matches = matches;

            OnMatchedChanged(newMask, Matches);
        }

        protected abstract void OnMatchedChanged(Mask newMask, bool matches);
        protected abstract void SetImmediate(Mask mask, bool matches);
    }
}