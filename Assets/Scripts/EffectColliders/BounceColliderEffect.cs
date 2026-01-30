using UnityEngine;

namespace Overcrowded
{
    [RequireComponent(typeof(Collider))]
    public class BounceColliderEffect : TriggerColliderEffectBase
    {
        [SerializeField] private float _force;

        private int _playerLayer;

        public Vector3 Direction => transform.up;

        private void Awake()
        {
             _playerLayer = LayerMask.NameToLayer("Player");
        }

        protected override bool TryActivate(Collider other)
        {
            if (other.gameObject.layer != _playerLayer)
                return false;

            if (!other.TryGetComponent<FPSController>(out var controller))
                return false;

            Bounce(controller);
            return true;
        }

        protected override bool TryDeactivate(Collider other)
        {
            return true;
        }

        private void Bounce(FPSController playerController)
        {
            Debug.Log("Bouncing player");
            playerController.AddVelocity(Direction * _force);
        }
    }
}
