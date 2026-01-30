using System;
using UnityEngine;

namespace Overcrowded.Animations
{
    [Serializable]
    public class MaskToolbarConfigs
    {
        [SerializeField] private Item _owned;
        [SerializeField] private Item _notOwned;

        public Item GetItem(bool isOwned) => isOwned ? _owned : _notOwned;

        [Serializable]
        public class Item
        {
            [SerializeField] private Color _iconColor = Color.white;
            public Color IconColor => _iconColor;

            [SerializeField] private Color _backgroundColor = Color.white;
            public Color BackgroundColor => _backgroundColor;
        }
    }
}