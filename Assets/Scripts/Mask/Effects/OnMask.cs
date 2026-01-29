using UnityEngine;
using UnityEngine.Events;

namespace Overcrowded
{
    public class OnMask : OnMaskBase
    {
        [SerializeField] private UnityEvent _matches;
        [SerializeField] private UnityEvent _doesntMatch;

        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
            if (matches)
                _matches.Invoke();
            else
                _doesntMatch.Invoke();
        }
    }
}