using UnityEngine;

namespace Overcrowded
{
    public class CanvasFadeOutOnMask : FadeOutOnMaskBase
    {
        [SerializeField] private CanvasGroup[] _canvasGroups;

        protected override void SetAlpha(float alpha)
        {
            foreach (var cg in _canvasGroups) 
                cg.alpha = alpha;
        }
    }
}