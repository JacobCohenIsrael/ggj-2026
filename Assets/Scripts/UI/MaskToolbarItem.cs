using Overcrowded.Animations;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded
{
    internal class MaskToolbarItem : MonoBehaviour
    {
        [SerializeField] private Mask _mask;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _background;

        [Inject] private VisualConfigs _visualConfigs;
        [Inject] private MaskInventory _inventory;

        public Mask Mask => _mask;
        public Image Icon => _icon;
        public RectTransform RectTransform => (RectTransform)transform;

        private void Awake()
        {
            _icon.sprite = _mask.Icon;

            var owned = _inventory.OwnsMask(_mask);
            var visualConfig = _visualConfigs.MaskToolbarConfigs.GetItem(owned);

            _icon.color = visualConfig.IconColor;
            _background.color = visualConfig.BackgroundColor;
        }
    }
}