using UnityEngine;

namespace Overcrowded
{
    public class SlideColliderEffect : TriggerColliderEffectBase
    {
        private int _playerLayer;

        private void Awake()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
        }

        protected override bool TryActivate(Collider other)
        {
            if(other.gameObject.layer != _playerLayer)
                return false;

            if(!other.TryGetComponent<FPSController>(out var controller))
                return false;

            controller.SetSliding(true);
            return true;
        }

        protected override bool TryDeactivate(Collider other)
        {
            if(other.gameObject.layer != _playerLayer)
                return false;

            if(!other.TryGetComponent<FPSController>(out var controller))
                return false;

            controller.SetSliding(false);
            return true;
        }
    }
}