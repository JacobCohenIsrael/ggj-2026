using System;
using JetBrains.Annotations;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class MaskChanger
    {
        [Inject] private readonly MaskChangerConfigs _changerConfigs;
        private readonly MaskInventory _inventory;

        public event Action<Mask> OnMaskChanged;

        public Mask CurrentMask { get; private set; }

        private int _blockRefCount = 0;
        private double _lastChangeTime = float.NegativeInfinity;

        public MaskChanger(MaskInventory maskInventory)
        {
            _inventory = maskInventory;
            CurrentMask = _inventory.DefaultMask;
        }

        [UsedImplicitly]
        public IDisposable BlockMaskChanges()
        {
            return new MaskChangeBlocker(this);
        }

        public bool TrySetMask(Mask newMask)
        {
            if (!CanChange(newMask))
                return false;

            CurrentMask = newMask;

            _lastChangeTime = Time.timeAsDouble;

            OnMaskChanged?.Invoke(newMask);

            return true;
        }

        public bool CanChange(Mask newMask)
        {
            if(_blockRefCount > 0)
                return false;

            if(Time.timeAsDouble - _lastChangeTime < _changerConfigs.MaskChangeCooldown)
                return false;

            if (CurrentMask == newMask)
                return false;

            if (!_inventory.OwnsMask(newMask))
                return false;

            return true;
        }

        private class MaskChangeBlocker : IDisposable
        {
            private readonly MaskChanger _maskChanger;

            public MaskChangeBlocker(MaskChanger maskChanger)
            {
                _maskChanger = maskChanger;
                _maskChanger._blockRefCount++;
            }

            public void Dispose()
            {
                _maskChanger._blockRefCount--;
            }
        }
    }
}