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
        [SerializeField] private Ease _ease = Ease.OutCubic;

        [Inject] private MaskChanger _maskChanger;

        private void Awake()
        {
            var mask = _maskChanger.CurrentMask;

            foreach (var toolbarItem in _maskToolbarItem)
            {
                if(toolbarItem.Mask == mask)
                    toolbarItem.RectTransform.sizeDelta = _selectedSize * Vector2.one;
                else
                    toolbarItem.RectTransform.sizeDelta = _deselectedSize * Vector2.one;
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
                else
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
            item.Icon.DOColor(Color.gray, _duration).SetEase(_ease);

            item.RectTransform.DOKill();
            item.RectTransform.DOSizeDelta(_deselectedSize * Vector2.one, _duration).SetEase(_ease);
        }
    }
}
