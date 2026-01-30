namespace Overcrowded
{
    public class EnableOnMask : OnMaskBase
    {
        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
            gameObject.SetActive(matches);
        }

        protected override void SetImmediate(Mask mask, bool matches)
        {
            gameObject.SetActive(matches);
        }
    }
}