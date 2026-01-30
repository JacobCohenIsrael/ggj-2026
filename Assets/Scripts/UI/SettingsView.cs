using Overcrowded.Services;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Overcrowded.Game.UI.MainMenu
{

    public class SettingsView : MonoBehaviour
    {
        private const string SfxVolume = "SfxVolume";
        private const string MusicVolume = "MusicVolume";

        [FormerlySerializedAs("sfxSlider")] [SerializeField] private Slider _sfxSlider;
        [FormerlySerializedAs("musicSlider")] [SerializeField] private Slider _musicSlider;
        [FormerlySerializedAs("audioMixer")] [SerializeField] private AudioMixer _audioMixer;
        
        [Inject]
        private LocalStorage _localStorage;
        
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
            _sfxSlider.value = _localStorage.SfxVolume;
            _musicSlider.value = _localStorage.MusicVolume;
            _audioMixer.SetFloat(SfxVolume, _localStorage.SfxVolume);
            _audioMixer.SetFloat(MusicVolume, _localStorage.MusicVolume);
        }
        
        private void OnEnable()
        {
            _sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);
            _musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        }

        private void OnDisable()
        {
            _sfxSlider.onValueChanged.RemoveListener(OnSfxSliderChanged);
            _musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        }

        private void OnSfxSliderChanged(float value)
        {
            _localStorage.SfxVolume = value;
            _audioMixer.SetFloat(SfxVolume, ToDecibels(value));
        }

        private void OnMusicSliderChanged(float value)
        {
            _localStorage.MusicVolume = value;
            _audioMixer.SetFloat(MusicVolume, ToDecibels(value));
        }
        
        private static float ToDecibels(float value01)
        {
            value01 = Mathf.Clamp(value01, 0.0001f, 1f); // avoid log(0)
            return Mathf.Log10(value01) * 20f;           // 0..1 -> -80..0-ish
        }
    }
}
