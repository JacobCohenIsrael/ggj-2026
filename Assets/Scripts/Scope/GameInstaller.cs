using Overcrowded.Animations;
using Reflex.Core;
using UnityEngine;

namespace Overcrowded
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private AnimationConfigs _animationConfigs;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(new MaskChanger());
            containerBuilder.RegisterValue(_animationConfigs);
        }
    }
}
