using UnityEngine;

namespace Overcrowded.Animations
{
    [CreateAssetMenu(menuName = "Create VisualConfigs", fileName = "VisualConfigs", order = 0)]
    public class VisualConfigs : ScriptableObject
    {
        [SerializeField] private MaskToolbarConfigs _maskToolbarConfigs;
        public MaskToolbarConfigs MaskToolbarConfigs => _maskToolbarConfigs;
    }
}