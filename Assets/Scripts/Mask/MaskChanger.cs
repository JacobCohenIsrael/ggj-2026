using System;

namespace Overcrowded
{
    public class MaskChanger
    {
        public event Action<Mask> OnMaskChanged;

        public Mask CurrentMask { get; private set; }

        public void SetMask(Mask newMask)
        {
            if (CurrentMask == newMask)
                return;

            CurrentMask = newMask;

            OnMaskChanged?.Invoke(newMask);
        }
    }
}