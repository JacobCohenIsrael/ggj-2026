using UnityEngine;

namespace Overcrowded
{
    public class ComponentEnableOnMask : OnMaskBase
    {
        [SerializeField] private Behaviour[] _components;
        [SerializeField] private bool _initialState = false;

        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
            var state = _initialState ? !matches : matches;
            foreach (var component in _components)
            {
                component.enabled = state;
            }
        }
    }
}