using System;

namespace Overcrowded
{
    public class MaskChanger
    {
        public event Action<Mask> OnMaskChanged;

        public void SetMask(Mask newMask)
        {
            OnMaskChanged?.Invoke(newMask);
        }
    }
}