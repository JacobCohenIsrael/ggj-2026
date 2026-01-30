using System;
using UnityEngine;
using UnityEngine.UI;

namespace Overcrowded.Game.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private SettingsView settingsView;
        [SerializeField] private Button settingsButton;

        private void Start()
        {
            settingsView.Hide();
        }

        private void OnEnable()
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            settingsView.OnClose += OnSettingsCloseRequested;
        }

        private void OnDisable()
        {
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            settingsView.OnClose -= OnSettingsCloseRequested;
        }

        private void OnSettingsButtonClicked()
        {
            settingsView.Show();
        }

        private void OnSettingsCloseRequested()
        {
            settingsView.Hide();
        }
    }
}