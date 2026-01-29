using System.Collections.Generic;
using JetBrains.Annotations;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public abstract class OnMaskBase : MonoBehaviour
    {
        [SerializeField] private List<Mask> _masks;

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

        private void HandleMaskChanged(Mask newMask)
        {
            Mask = newMask;
            Matches = _masks.Contains(newMask);
            OnMatchedChanged(newMask, Matches);
        }

        protected abstract void OnMatchedChanged(Mask newMask, bool matches);
    }
}