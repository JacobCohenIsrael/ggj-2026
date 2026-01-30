using UnityEngine;

namespace Overcrowded
{
    public class RendererFadeOutOnMask : FadeOutOnMaskBase
    {
        [SerializeField] private Renderer[] _renderers;

        protected override void SetAlpha(float alpha)
        {
            foreach (var rend in _renderers)
            {
                foreach (var mat in rend.materials)
                {
                    var color = mat.color;
                    color.a = alpha;
                    mat.color = color;
                }
            }
        }
    }
}