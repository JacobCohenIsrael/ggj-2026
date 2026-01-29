using Reflex.Core;
using UnityEngine;

namespace Overcrowded
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(new MaskChanger());
        }
    }
}
