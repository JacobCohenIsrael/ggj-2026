using System;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    [RequireComponent(typeof(Collider))]
    public class BlockMaskChangeCollider : MonoBehaviour
    {
        [Inject] private MaskChanger _maskChanger;

        private IDisposable _changeBlocker = null;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Blocking mask changes");

            _changeBlocker = _maskChanger.BlockMaskChanges();
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Unblocking mask changes");
            
            _changeBlocker?.Dispose();
        }
    }
}
