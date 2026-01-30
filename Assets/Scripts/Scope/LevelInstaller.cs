using Overcrowded.Services;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Overcrowded
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MaskInventory _inventory;
        [SerializeField] private MaskChangerConfigs _changerConfigs;
        [SerializeField] private LevelMenuView _levelMenuView;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(_inventory);
            containerBuilder.RegisterType(typeof(MaskChanger), Lifetime.Singleton, Resolution.Eager);
            containerBuilder.RegisterType(typeof(PlayerDeathService), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterValue(_changerConfigs);
            containerBuilder.RegisterValue(_levelMenuView);
        }
    }
}