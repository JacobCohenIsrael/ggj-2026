using Overcrowded.Game.UI.MainMenu;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded.Services
{
    public class PlayerWonService : MonoBehaviour
    {
        [SerializeField] private AudioClip levelWonClip;
        
        [Inject] private UserState _userState;
        [Inject] private DarkOverlayController _darkOverlay;
        [Inject] private LevelLoader _levelLoader;
        [Inject] private AudioManager _audioManager;
        
        public void HandlePlayerWon(bool gainedMask, bool isFinalLevel)
        {
            //todo pretty animation 
            _audioManager.PlaySfx(levelWonClip, 0.3f);

            var sceneName = gameObject.scene.name;

            if(sceneName != "TestLevel")
            {
                var currentLevel = int.Parse(sceneName["Level_".Length..]);
                _userState.SetLevelCompleted(currentLevel);

                if (isFinalLevel)
                    _levelLoader.LoadLevel("MainMenu", _darkOverlay.LevelComplete);
                else
                    _levelLoader.LoadLevel(currentLevel + 1, _darkOverlay.LevelComplete);
            }
            else
            {
                _levelLoader.LoadLevel("MainMenu", _darkOverlay.LevelComplete);
            }

            _darkOverlay.CreateGoodJobTween();

            if(gainedMask)
                _darkOverlay.CreatedYouGainedAMaskTween();
        }
    }
}