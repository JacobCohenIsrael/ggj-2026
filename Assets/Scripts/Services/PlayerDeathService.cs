using DG.Tweening;
using Overcrowded.Game.UI.MainMenu;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overcrowded.Services
{
    public class PlayerDeathService
    {
        [Inject] private DarkOverlayController _darkOverlay;
        [Inject] private LevelLoader _levelLoader;

        private bool _dead;

        public void HandlePlayerDeath(PlayerView player)
        {
            if (_dead)
                return;
            _dead = true;

            // Logic to handle player death, e.g., respawn, update stats, etc.
            Debug.Log("Player has died. Handling death logic...");

            _levelLoader.LoadLevel(player.gameObject.scene.name, _darkOverlay.Death);
        }
    }
}