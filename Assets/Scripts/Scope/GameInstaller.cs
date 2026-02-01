using Overcrowded.Animations;
using Overcrowded.Game.UI.MainMenu;
using Overcrowded.Services;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Overcrowded.Scope
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private VisualConfigs _visualConfigs;
        [SerializeField] private InputConfigs _inputConfigs;
        [SerializeField] private DarkOverlayController _darkOverlayPrefab;
        [SerializeField] private AudioManager _audioManagerPrefab;
        [SerializeField] private AudioLibrary _audioLibrary;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterValue(new LocalStorage());
            containerBuilder.RegisterType(typeof(UserState), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(LevelLoader), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterValue(_visualConfigs);
            containerBuilder.RegisterValue(_inputConfigs);
            containerBuilder.RegisterValue(_audioLibrary);
            containerBuilder.RegisterFactory(_ => Instantiate(_audioManagerPrefab), Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterFactory(_ => Instantiate(_darkOverlayPrefab), Lifetime.Singleton, Resolution.Eager);
        }
    }
}
