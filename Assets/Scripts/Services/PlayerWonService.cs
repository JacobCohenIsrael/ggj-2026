using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overcrowded.Services
{
    public class PlayerWonService : MonoBehaviour
    {
        [Inject] private UserState _userState;
        
        public void HandlePlayerWon()
        {
            //todo pretty animation 
            //todo play some SFX
            _userState.IncreaseLevel();
            var currentLevel = int.Parse(gameObject.scene.name["Level_".Length..]);
            SceneManager.LoadScene($"Level_{currentLevel + 1}", LoadSceneMode.Single);
        }
    }
}