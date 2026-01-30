using Overcrowded.Animations;
using Reflex.Core;
using UnityEngine;

namespace Overcrowded
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MaskRegistry _maskRegistry;
        [SerializeField] private MaskEffectsVisualConfigs _maskEffectsVisualConfigs;
        [SerializeField] private VisualConfigs _visualConfigs;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(_maskRegistry);
            containerBuilder.RegisterValue(new MaskChanger());
            containerBuilder.RegisterValue(_animationConfigs);
            containerBuilder.RegisterValue(typeof(UserState));
            containerBuilder.RegisterValue(_maskEffectsVisualConfigs);
            containerBuilder.RegisterValue(_visualConfigs);
        }
    }
}
