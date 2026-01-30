using UnityEngine;

namespace Overcrowded
{
    public class ComponentEnableOnMask : OnMaskBase
    {
        [SerializeField] private Behaviour[] _components;

        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
            foreach (var component in _components)
                component.enabled = matches;
        }
    }
}