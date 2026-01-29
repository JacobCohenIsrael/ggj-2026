using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class MaskSetter : MonoBehaviour
    {
        [SerializeField] private Mask _mask;

        [Inject] private MaskChanger _maskChanger;

        public void Set()
        {
            _maskChanger.SetMask(_mask);
        }
    }
}