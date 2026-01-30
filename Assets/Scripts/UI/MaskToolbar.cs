using System.Linq;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class MaskToolbar : MonoBehaviour
    {
        [SerializeField] private Transform _masksContainer;
        [SerializeField] private MaskToolbarItem _maskItemPrefab;

        [Inject] private MaskRegistry _maskRegistry;
        [Inject] private MaskInventory _maskInventory;

        private void Awake()
        {
            foreach (var mask in _maskRegistry.AllMasks)
                CreateMaskItem(mask);
        }

        private bool IsOwned(Mask mask) => _maskInventory.OwnedMasks.Contains(mask);

        private MaskToolbarItem CreateMaskItem(Mask mask)
        {
            var item = Instantiate(_maskItemPrefab, _masksContainer);
            var owned = IsOwned(mask);
            item.Initialize(mask, owned);
            return item;
        }
    }
}
