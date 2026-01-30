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
        [SerializeField] private OnMaskBase[] _effects;

        [Inject] private VisualConfigs _visualConfigs;

        public void Initialize(Mask mask, bool owned)
        {
            _icon.sprite = mask.Icon;

            var visualConfig = _visualConfigs.MaskToolbarConfigs.GetItem(owned);

            _icon.color = visualConfig.IconColor;
            _background.color = visualConfig.BackgroundColor;

            foreach (var effect in _effects)
                effect.AddMask(mask);
        }
    }
}