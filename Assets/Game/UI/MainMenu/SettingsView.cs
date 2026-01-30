using System;
using Overcrowded.Services;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Overcrowded.Game.UI.MainMenu
{
    public class SettingsView : MonoBehaviour
    {
        private const string SfxVolume = "SfxVolume";
        private const string MusicVolume = "MusicVolume";
        
        public Action OnClose;
        
        [SerializeField] private Button closeButton;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private AudioMixer audioMixer;
        
        [Inject]
        private LocalStorage localStorage;
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        private void Start()
        {
            sfxSlider.value = localStorage.SfxVolume;
            musicSlider.value = localStorage.MusicVolume;
            audioMixer.SetFloat(SfxVolume, localStorage.SfxVolume);
            audioMixer.SetFloat(MusicVolume, localStorage.MusicVolume);
        }
        
        private void OnEnable()
        {
            closeButton.onClick.AddListener(OnCloseButtonClicked);
            sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            sfxSlider.onValueChanged.RemoveListener(OnSfxSliderChanged);
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        }

        private void OnCloseButtonClicked()
        {
            OnClose?.Invoke();
        }

        private void OnSfxSliderChanged(float value)
        {
            localStorage.SfxVolume = value;
            audioMixer.SetFloat(SfxVolume, ToDecibels(value));
        }

        private void OnMusicSliderChanged(float value)
        {
            localStorage.MusicVolume = value;
            audioMixer.SetFloat(MusicVolume, ToDecibels(value));
        }
        
        private static float ToDecibels(float value01)
        {
            value01 = Mathf.Clamp(value01, 0.0001f, 1f); // avoid log(0)
            return Mathf.Log10(value01) * 20f;           // 0..1 -> -80..0-ish
        }
    }
}
