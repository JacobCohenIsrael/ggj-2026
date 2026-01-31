using DG.Tweening;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    [RequireComponent(typeof(RectTransform))]
    public class MaskToolbarAnimator : MonoBehaviour
    {
        [SerializeField] private MaskToolbarItem[] _maskToolbarItem;

        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _selectedSize = 150f;
        [SerializeField] private float _deselectedSize = 100f;
        [SerializeField] private Color _unselectedColor = Color.gray;
        [SerializeField] private Color _lockedColor = Color.black;
        [SerializeField] private Ease _ease = Ease.OutCubic;

        [Inject] private MaskChanger _maskChanger;
        [Inject] private MaskInventory _maskInventory;

        private void Awake()
        {
            var mask = _maskChanger.CurrentMask;

            foreach (var toolbarItem in _maskToolbarItem)
            {
                if(toolbarItem.Mask == mask)
                {
                    toolbarItem.RectTransform.sizeDelta = _selectedSize * Vector2.one;
                    toolbarItem.Icon.color = Color.white;
                }
                else
                {
                    toolbarItem.RectTransform.sizeDelta = _deselectedSize * Vector2.one;
                    toolbarItem.Icon.color = _maskInventory.OwnsMask(toolbarItem.Mask) ? _unselectedColor : _lockedColor;
                }
            }
        }

        private void OnEnable()
        {
            _maskChanger.SubscribeMaskChanged(OnMaskChanged, true);
        }

        private void OnDisable()
        {
            _maskChanger.UnsubscribeMaskChanged(OnMaskChanged, true);
        }

        private void OnMaskChanged(Mask mask)
        {
            foreach (var toolbarItem in _maskToolbarItem)
            {
                if(toolbarItem.Mask == mask)
                    TweenSelect(toolbarItem);
                else if(_maskInventory.OwnsMask(toolbarItem.Mask))
                    TweenDeselect(toolbarItem);
            }
        }

        private void TweenSelect(MaskToolbarItem item)
        {
            item.Icon.DOKill();
            item.Icon.DOColor(Color.white, _duration).SetEase(_ease);

            item.RectTransform.DOKill();
            item.RectTransform.DOSizeDelta(_selectedSize * Vector2.one, _duration).SetEase(_ease);
        }

        private void TweenDeselect(MaskToolbarItem item)
        {
            item.Icon.DOKill();
            item.Icon.DOColor(_unselectedColor, _duration).SetEase(_ease);

            item.RectTransform.DOKill();
            item.RectTransform.DOSizeDelta(_deselectedSize * Vector2.one, _duration).SetEase(_ease);
        }
    }
}
