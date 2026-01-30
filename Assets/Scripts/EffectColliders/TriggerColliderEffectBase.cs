using UnityEngine;

namespace Overcrowded
{
    public abstract class TriggerColliderEffectBase : MonoBehaviour
    {
        [SerializeField] private double _cooldown = 2f;

        private double _lastEffectTime = -Mathf.Infinity;
        private bool _activatedThisTrigger;

        private void OnTriggerStay(Collider other)
        {
            if(!CanActivate())
                return;

            if (TryActivate(other))
            {
                _lastEffectTime = Time.timeAsDouble;
                _activatedThisTrigger = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            _activatedThisTrigger = false;
        }

        protected virtual bool CanActivate()
        {
            return Time.timeAsDouble - _lastEffectTime >= _cooldown && !_activatedThisTrigger;
        }

        protected abstract bool TryActivate(Collider collider);
    }
}