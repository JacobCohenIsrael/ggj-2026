using System;
using UnityEngine;

namespace Overcrowded
{
    [Serializable]
    public class InputConfigs
    {
        [SerializeField] private KeyTrigger _maskSelectionTrigger;
        public KeyTrigger MaskSelectionTrigger => _maskSelectionTrigger;
    }
}