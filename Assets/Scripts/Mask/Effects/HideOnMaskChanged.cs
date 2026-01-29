namespace Overcrowded
{
    public class HideOnMaskChanged : OnMaskBase
    {
        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
            gameObject.SetActive(matches);
        }
    }
}