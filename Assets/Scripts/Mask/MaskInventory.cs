using System;
using System.Collections.Generic;
using UnityEngine;

namespace Overcrowded
{
    [Serializable]
    public class MaskInventory
    {
        [SerializeField] private Mask[] _ownedMasks;
        public IReadOnlyList<Mask> OwnedMasks => _ownedMasks;
    }
}