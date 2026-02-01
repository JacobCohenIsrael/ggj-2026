using UnityEngine;

namespace Overcrowded
{
    public class MaterialFadeOutOnMask : FadeOutOnMaskBase
    {
        [SerializeField] private Material[] _materials;

        protected override void SetAlpha(float alpha)
        {
            foreach (var m in _materials)
            {
                var color = m.color;
                color.a = alpha;
                m.color = color;
            }
        }
    }
}