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
            SceneManager.LoadScene($"Level_{_userState.Level + 1}", LoadSceneMode.Single);
        }
    }
}