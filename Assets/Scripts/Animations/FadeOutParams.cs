using System;
using UnityEngine;

namespace Overcrowded.Animations
{
    [Serializable]
    public class FadeOutParams
    {
        [SerializeField] private float _duration = 1f;
        public float Duration => _duration;

        [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve AnimationCurve => _animationCurve;
    }
}