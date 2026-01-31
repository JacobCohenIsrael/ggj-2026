using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Overcrowded.Services;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class MaskChanger
    {
        [Inject] private readonly MaskChangerConfigs _changerConfigs;
        [Inject] private AudioManager _audioManager;
        [Inject] private AudioLibrary _audioLibrary;

        private readonly MaskInventory _inventory;

        private event Action<Mask> OnMaskChangedWithoutDelay;
        private event Action<Mask> OnMaskChanged;

        public void SubscribeMaskChanged(Action<Mask> callback, bool ignoreDelay = false)
        {
            if (ignoreDelay)
                OnMaskChangedWithoutDelay += callback;
            else
                OnMaskChanged += callback;
        }

        public void UnsubscribeMaskChanged(Action<Mask> callback, bool ignoreDelay = false)
        {
            if (ignoreDelay)
                OnMaskChangedWithoutDelay -= callback;
            else
                OnMaskChanged -= callback;
        }

        public Mask CurrentMask { get; private set; }

        private int _blockRefCount = 0;
        private double _lastChangeTime = float.NegativeInfinity;

        private CancellationTokenSource _changeCancel = new();

        public MaskChanger(MaskInventory maskInventory)
        {
            _inventory = maskInventory;
            CurrentMask = _inventory.DefaultMask;
        }

        [UsedImplicitly]
        public IDisposable BlockMaskChanges()
        {
            return new MaskChangeBlocker(this);
        }

        public void RequestSetMask(Mask newMask)
        {
            if (!CanChange(newMask))
                return;

            _changeCancel.Cancel();
            _changeCancel.Dispose();
            _changeCancel = new();

            CurrentMask = newMask;
            _lastChangeTime = Time.timeAsDouble;
            OnMaskChangedWithoutDelay?.Invoke(newMask);

            ChangeAfterDelay(newMask, _changerConfigs.Delay, _changeCancel.Token)
                .Forget();
        }

        private UniTask ChangeAfterDelay(Mask newMask, double delay, CancellationToken cancellationToken)
        {
            if (delay > Mathf.Epsilon)
            {
                return UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken)
                    .ContinueWith(() => ChangeMask(newMask));
            }

            ChangeMask(newMask);
            return UniTask.CompletedTask;
        }

        private void ChangeMask(Mask newMask)
        {
            _audioManager.PlaySfx(_audioLibrary.MaskChangeClip, 0.25f);
            OnMaskChanged?.Invoke(newMask);
        }

        public bool CanChange(Mask newMask)
        {
            if(_blockRefCount > 0)
                return false;

            if(Time.timeAsDouble - _lastChangeTime < _changerConfigs.MaskChangeCooldown)
                return false;

            if (CurrentMask == newMask)
                return false;

            if (!_inventory.OwnsMask(newMask))
                return false;

            return true;
        }

        private class MaskChangeBlocker : IDisposable
        {
            private readonly MaskChanger _maskChanger;

            public MaskChangeBlocker(MaskChanger maskChanger)
            {
                _maskChanger = maskChanger;
                _maskChanger._blockRefCount++;
            }

            public void Dispose()
            {
                _maskChanger._blockRefCount--;
            }
        }
    }
}