using Overcrowded.Services;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class HeightAutoKill : MonoBehaviour
    {
        [SerializeField] private PlayerView _player;
        [SerializeField] private float _killHeight = -10f;

        [Inject] private PlayerDeathService _playerDeathService;

        private void Update()
        {
            if (_player.transform.position.y >= _killHeight)
                return;

            _playerDeathService.HandlePlayerDeath(_player);
        }
    }
}
