using Overcrowded.Animations;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded
{
    internal class MaskToolbarItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _background;

        [SerializeField] private GameObject _ownedIndicator;

        [Inject] private VisualConfigs _visualConfigs;
        [Inject] private MaskChanger _maskChanger;

        private Mask _mask;

        private void Awake()
        {
            _maskChanger.OnMaskChanged += OnMaskChanged;
        }

        private void OnDestroy()
        {
            _maskChanger.OnMaskChanged -= OnMaskChanged;
        }

        private void OnMaskChanged(Mask mask)
        {
            _ownedIndicator.SetActive(_mask == mask);
        }

        public void Initialize(Mask mask, bool owned)
        {
            _mask = mask;

            _icon.sprite = mask.Icon;

            var visualConfig = _visualConfigs.MaskToolbarConfigs.GetItem(owned);

            _icon.color = visualConfig.IconColor;
            _background.color = visualConfig.BackgroundColor;
        }
    }
}