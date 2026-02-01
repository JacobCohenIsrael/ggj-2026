using UnityEngine;

namespace Overcrowded.Animations
{
    [CreateAssetMenu(menuName = "Overcrowded/Create AnimationConfigs", fileName = "AnimationConfigs", order = 0)]
    public class MaskEffectsVisualConfigs : ScriptableObject
    {
        [SerializeField] private FadeOutParams _fadeOutParams;
        public FadeOutParams FadeOutParams => _fadeOutParams;
    }
}