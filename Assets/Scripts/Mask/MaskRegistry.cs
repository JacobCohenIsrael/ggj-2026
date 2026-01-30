using System;
using System.Collections.Generic;
using UnityEngine;

namespace Overcrowded
{
    [Serializable]
    public class MaskRegistry
    {
        [SerializeField] private Mask[] _allMasks;
        public IReadOnlyList<Mask> AllMasks => _allMasks;
    }
}