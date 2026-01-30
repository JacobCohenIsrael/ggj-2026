using System;
using System.Collections.Generic;
using UnityEngine;

namespace Overcrowded
{
    [Serializable]
    public class MaskInventory
    {
        [SerializeField] private List<Mask> _ownedMasks;
        public IReadOnlyList<Mask> OwnedMasks => _ownedMasks;

        public bool OwnsMask(Mask mask) => _ownedMasks.Contains(mask);
    }
}