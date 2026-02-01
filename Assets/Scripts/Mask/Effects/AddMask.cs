using UnityEngine;

namespace Overcrowded
{
    public class AddMask : MonoBehaviour
    {
        [SerializeField] private Mask[] _masks;
        [SerializeField] private OnMaskBase[] _effects;

        private void Awake()
        {
            foreach (var effect in _effects)
                effect.AddMasks(_masks);
        }
    }
}