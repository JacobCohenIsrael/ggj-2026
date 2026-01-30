using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded.Services
{
    public class PlayerWonService : MonoBehaviour
    {
        //[SerializeField] LevelMenuView _levelMenuView;
        [Inject] private UserState _userState;
        
        public void HandlePlayerWon(PlayerView player)
        {
             
            _userState.IncreaseLevel();
            //SceneManager.LoadScene(player.gameObject.scene.name, LoadSceneMode.Single);
        }
    }
}