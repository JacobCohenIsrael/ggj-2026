using System;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded.Game.UI
{
    public class SettingsView : MonoBehaviour
    {
        public Action OnClose;
        
        [SerializeField] private Button closeButton;

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked()
        {
            OnClose?.Invoke();
        }
    }
}
