using System;
using UnityEngine;

namespace Overcrowded
{
    [Serializable]
    public class MaskChangerConfigs
    {
        [SerializeField] private float _delay;
        public float Delay => _delay;

        [SerializeField] private double _maskChangeCooldown = 0.5f;
        public double MaskChangeCooldown => _maskChangeCooldown;
    }
}