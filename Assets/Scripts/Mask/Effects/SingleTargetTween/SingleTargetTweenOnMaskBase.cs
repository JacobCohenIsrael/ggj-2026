namespace Overcrowded.SingleTargetTween
{
    public abstract class SingleTargetTweenOnMaskBase<T> : SingleTargetOnMaskBase
    {
        public abstract T Current { get; }
        public T Initial { get; private set; }
        public T Target { get; private set; }
        
        protected abstract void Set(T value);
        protected abstract T UpdateMoveTowards(T targetValue);
        protected abstract T GetTargetForMask(Mask mask);

        protected override void Awake()
        {
            base.Awake();
            
            Initial = Current;
            Target = Current;
        }

        private void Update()
        {
            var newValue = UpdateMoveTowards(Target);
            Set(newValue);
        }

        protected override void HandleMaskChanged(Mask mask)
        {
            Target = GetTargetForMask(mask);
        }

        protected override void SetImmediate(Mask mask)
        {
            Target = GetTargetForMask(mask);
            Set(Target);
        }
    }
}