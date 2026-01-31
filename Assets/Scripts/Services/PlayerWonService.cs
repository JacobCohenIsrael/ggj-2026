using Overcrowded.Game.UI.MainMenu;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded.Services
{
    public class PlayerWonService : MonoBehaviour
    {
        [Inject] private UserState _userState;
        [Inject] private DarkOverlayController _darkOverlay;
        [Inject] private LevelLoader _levelLoader;
        
        public void HandlePlayerWon(bool gainedMask)
        {
            //todo pretty animation 
            //todo play some SFX
            var currentLevel = int.Parse(gameObject.scene.name["Level_".Length..]);
            _userState.SetLevelCompleted(currentLevel);

            _levelLoader.LoadLevel(currentLevel + 1, _darkOverlay.LevelComplete);

            _darkOverlay.CreateGoodJobTween();

            if(gainedMask)
                _darkOverlay.CreatedYouGainedAMaskTween();
        }
    }
}