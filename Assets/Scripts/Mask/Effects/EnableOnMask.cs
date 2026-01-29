using UnityEngine;

namespace Overcrowded
{
    public class EnableOnMask : OnMaskBase
    {
        [SerializeField] private bool _initialState = false;

        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
            var state = _initialState ? !matches : matches;
            gameObject.SetActive(state);
        }
    }
}