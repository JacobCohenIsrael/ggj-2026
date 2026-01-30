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

        private void Bounce(FPSController playerController)
        {
            playerController.AddVelocity(Direction * _force);
        }
    }
}
