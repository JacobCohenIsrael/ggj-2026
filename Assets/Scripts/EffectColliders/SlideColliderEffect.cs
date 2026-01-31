using System.Collections;
using Overcrowded.Services;
using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded.EffectColliders
{
    public class SlideColliderEffect : TriggerColliderEffectBase
    {
        [SerializeField] private AudioSource slideAudioSource;
        [SerializeField] private float slideFadeDuration = 0.3f;
        
        [Inject]
        private AudioLibrary _audioLibrary;
        
        private int _playerLayer;
        private Coroutine slideFadeCoroutine;

        private void Awake()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
            slideAudioSource.clip = _audioLibrary.SlideEffectClipRecord.Clip;
        }

        protected override bool TryActivate(Collider other)
        {
            if(other.gameObject.layer != _playerLayer)
                return false;

            if(!other.TryGetComponent<FPSController>(out var controller))
                return false;

            // Fade in slide sound
            if (slideAudioSource != null)
            {
                if (slideFadeCoroutine != null)
                    StopCoroutine(slideFadeCoroutine);
                slideFadeCoroutine = StartCoroutine(FadeSlideAudio(true));
            }
            controller.SetSliding(true);
            return true;
        }

        protected override bool TryDeactivate(Collider other)
        {
            if(other.gameObject.layer != _playerLayer)
                return false;

            if(!other.TryGetComponent<FPSController>(out var controller))
                return false;

            // Fade out slide sound
            if (slideAudioSource != null)
            {
                if (slideFadeCoroutine != null)
                    StopCoroutine(slideFadeCoroutine);
                slideFadeCoroutine = StartCoroutine(FadeSlideAudio(false));
            }
            controller.SetSliding(false);
            return true;
        }

        private IEnumerator FadeSlideAudio(bool fadeIn)
        {
            var audioClipRecord = _audioLibrary.SlideEffectClipRecord;
            if (fadeIn)
            {
                slideAudioSource.volume = 0f;
                if (!slideAudioSource.isPlaying)
                    slideAudioSource.Play();
                float t = 0f;
                while (t < slideFadeDuration)
                {
                    slideAudioSource.volume = Mathf.Lerp(0f, audioClipRecord.Volume, t / slideFadeDuration);
                    t += Time.deltaTime;
                    yield return null;
                }
                slideAudioSource.volume = audioClipRecord.Volume;
            }
            else
            {
                float startVol = slideAudioSource.volume;
                float t = 0f;
                while (t < slideFadeDuration)
                {
                    slideAudioSource.volume = Mathf.Lerp(startVol, 0f, t / slideFadeDuration);
                    t += Time.deltaTime;
                    yield return null;
                }
                slideAudioSource.volume = 0f;
                slideAudioSource.Stop();
            }
        }
    }
}