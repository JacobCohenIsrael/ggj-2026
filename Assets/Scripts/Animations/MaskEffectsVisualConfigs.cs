using UnityEngine;

namespace Overcrowded.Animations
{
    [CreateAssetMenu(menuName = "Create AnimationConfigs", fileName = "AnimationConfigs", order = 0)]
    public class MaskEffectsVisualConfigs : ScriptableObject
    {
        [SerializeField] private FadeOutParams _fadeOutParams;
        public FadeOutParams FadeOutParams => _fadeOutParams;
    }
}