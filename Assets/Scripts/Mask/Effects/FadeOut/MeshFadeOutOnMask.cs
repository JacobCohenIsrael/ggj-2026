using UnityEngine;

namespace Overcrowded
{
    public class MeshFadeOutOnMask : FadeOutOnMaskBase
    {
        [SerializeField] private MeshRenderer[] _renderers;

        protected override void SetAlpha(float alpha)
        {
            Debug.Log("MeshFadeOutOnMask SetAlpha: " + alpha);

            foreach (var r in _renderers)
            {
                foreach (var m in r.materials)
                {
                    var color = m.color;
                    color.a = alpha;
                    m.color = color;
                }
            }
        }
    }
}