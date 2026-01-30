using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overcrowded.Services
{
    public class PlayerDeathService
    {
        public void HandlePlayerDeath(PlayerView player)
        {
            // Logic to handle player death, e.g., respawn, update stats, etc.
            Debug.Log("Player has died. Handling death logic...");

            SceneManager.LoadScene(player.gameObject.scene.name, LoadSceneMode.Single);
        }
    }
}