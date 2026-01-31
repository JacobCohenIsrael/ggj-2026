using Overcrowded.Game.UI.MainMenu;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded.Services
{
    public class LevelStarter : MonoBehaviour
    {
        [Inject] private DarkOverlayController _darkOverlay;

        private void Start()
        {
            _darkOverlay.CreateFadeOutTween(_darkOverlay.LevelStart);
        }
    }
}