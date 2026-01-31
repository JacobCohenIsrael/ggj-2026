using Overcrowded.Services;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class OnReachedEnd : MonoBehaviour
    {
        [SerializeField] private bool _gainedAMask;
        [SerializeField] private bool _isFinalLevel;

        [Inject] private PlayerWonService _playerWonService;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            _playerWonService.HandlePlayerWon(_gainedAMask, _isFinalLevel);
        }
    }
}