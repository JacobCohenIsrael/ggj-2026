using System;
using JetBrains.Annotations;
using Reflex.Attributes;

namespace Overcrowded
{
    public class MaskChanger
    {
        [Inject] private MaskInventory _inventory;

        public event Action<Mask> OnMaskChanged;

        public Mask CurrentMask { get; private set; }

        private int _blockRefCount = 0;

        [UsedImplicitly]
        public IDisposable BlockMaskChanges()
        {
            return new MaskChangeBlocker(this);
        }

        public bool TrySetMask(Mask newMask)
        {
            if (CurrentMask == newMask)
                return false;

            if(!_inventory.OwnsMask(newMask))
                return false;

            if(_blockRefCount > 0)
                return false;

            CurrentMask = newMask;

            OnMaskChanged?.Invoke(newMask);
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