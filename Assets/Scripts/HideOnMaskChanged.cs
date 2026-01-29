using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class HideOnMaskChanged : MonoBehaviour
    {
        [SerializeField] private Mask _maskToHideOn;

        [Inject] private MaskChanger _maskChanger;

        private void Awake()
        {
            _maskChanger.OnMaskChanged += HandleMaskChanged;
        }

        private void OnDestroy()
        {
            _maskChanger.OnMaskChanged -= HandleMaskChanged;
        }

        private void HandleMaskChanged(Mask newMask)
        {
            gameObject.SetActive(newMask != _maskToHideOn);
        }
    }
}