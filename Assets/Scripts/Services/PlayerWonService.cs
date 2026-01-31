using DG.Tweening;
using Overcrowded.Game.UI.MainMenu;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overcrowded.Services
{
    public class PlayerWonService : MonoBehaviour
    {
        [Inject] private UserState _userState;
        [Inject] private DarkOverlayController _darkOverlay;
        [Inject] private LevelLoader _levelLoader;
        
        public void HandlePlayerWon()
        {
            //todo pretty animation 
            //todo play some SFX
            var currentLevel = int.Parse(gameObject.scene.name["Level_".Length..]);
            _userState.SetLevelCompleted(currentLevel);

            _darkOverlay.CreateFadeInTween(_darkOverlay.LevelComplete)
                .OnComplete(() =>
                {
                    _levelLoader.LoadLevel(currentLevel + 1);
                });
        }
    }
}