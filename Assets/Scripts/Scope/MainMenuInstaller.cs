using Overcrowded.Services;
using Reflex.Core;
using UnityEngine;

namespace Overcrowded.Scope
{
    public class MainMenuInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(new LocalStorage());
        }
    }
}