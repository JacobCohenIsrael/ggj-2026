using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Overcrowded
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MaskInventory _inventory;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(_inventory);
            containerBuilder.RegisterType(typeof(MaskChanger), Lifetime.Singleton, Resolution.Eager);
        }
    }
}