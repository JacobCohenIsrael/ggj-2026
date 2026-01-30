using Reflex.Core;
using UnityEngine;

namespace Overcrowded
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MaskInventory _inventory;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(_inventory);
        }
    }
}