using UnityEngine;

namespace Overcrowded
{
    [CreateAssetMenu(menuName = "Create Mask", fileName = "Mask", order = 0)]
    public class Mask : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;
    }
}