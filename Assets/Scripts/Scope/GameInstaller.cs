using Overcrowded.Animations;
using Overcrowded.Services;
using Reflex.Core;
using UnityEngine;

namespace Overcrowded.Scope
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MaskRegistry _maskRegistry;
        [SerializeField] private VisualConfigs _visualConfigs;
        [SerializeField] private InputConfigs _inputConfigs;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(_maskRegistry);
            containerBuilder.RegisterValue(new LocalStorage());
            containerBuilder.RegisterValue(typeof(UserState));
            containerBuilder.RegisterValue(_visualConfigs);
            containerBuilder.RegisterValue(_inputConfigs);
        }
    }
}
