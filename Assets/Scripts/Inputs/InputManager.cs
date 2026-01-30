using System;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Mask[] _masks;

        [Inject] private InputConfigs _inputConfigs;
        [Inject] private MaskChanger _maskChanger;
        [Inject] private LevelMenuView _levelMenuView;

        private const int KeyCount = 10;

        private void Update()
        {
            CheckSettingsInputs();

            if (_levelMenuView.SettingsShown)
                return;

            CheckMaskInputs();
        }

        private void CheckSettingsInputs()
        {
            if (GetKeyActive(KeyCode.Escape))
                _levelMenuView.ToggleSettings();
        }

        private void CheckMaskInputs()
        {
            var selectedMaskIndex = GetInputMaskIndex();
            if (!selectedMaskIndex.HasValue)
                return;

            var index = selectedMaskIndex.Value;

            if(index < 0 || index >= _masks.Length)
                return;

            var mask = _masks[index];
            _maskChanger.TrySetMask(mask);
        }

        private int? GetInputMaskIndex()
        {
            for (var i = 0; i < KeyCount - 1; i++)
            {
                var keyCode = GetNumberKeycode(i + 1);
                if (GetKeyActive(keyCode))
                    return i;
            }

            if (GetKeyActive(0))
                return KeyCount - 1;

            return null;
        }

        private bool GetKeyActive(KeyCode keyCode)
        {
            return _inputConfigs.MaskSelectionTrigger switch
            {
                KeyTrigger.Down => Input.GetKeyDown(keyCode),
                KeyTrigger.Up => Input.GetKeyUp(keyCode),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private static KeyCode GetNumberKeycode(int number)
        {
            return number switch
            {
                1 => KeyCode.Alpha1,
                2 => KeyCode.Alpha2,
                3 => KeyCode.Alpha3,
                4 => KeyCode.Alpha4,
                5 => KeyCode.Alpha5,
                6 => KeyCode.Alpha6,
                7 => KeyCode.Alpha7,
                8 => KeyCode.Alpha8,
                9 => KeyCode.Alpha9,
                0 => KeyCode.Alpha0,
                _ => KeyCode.None,
            };
        }
    }
}
