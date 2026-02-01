using Overcrowded.Game.UI.MainMenu;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded.Services
{
    public class LevelStarter : MonoBehaviour
    {
        [Inject] private DarkOverlayController _darkOverlay;
        [Inject] private AudioLibrary _audioLibrary;
        [Inject] private AudioManager _audioManager;

        private void Start()
        {
            _darkOverlay.CreateFadeOutTween(_darkOverlay.LevelStart);
            _audioManager.ChangeMusic(_audioLibrary.LevelMusicClipRecord.Clip, 0.3f);
        }
    }
}