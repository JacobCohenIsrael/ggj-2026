namespace Overcrowded
{
    public class EnableOnMask : OnMaskBase
    {
        protected override void OnMatchedChanged(Mask newMask, bool matches)
        {
            gameObject.SetActive(matches);
        }
    }
}