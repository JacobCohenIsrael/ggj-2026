using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded
{
    public class ImageFadeOutOnMask : FadeOutOnMaskBase
    {
        [SerializeField] private Image[] _images;

        protected override void SetAlpha(float alpha)
        {
            foreach (var img in _images)
            {
                var color = img.color;
                color.a = alpha;
                img.color = color;
            }
        }
    }
}