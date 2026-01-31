using System.Collections;
using UnityEngine;

namespace Overcrowded.Services
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicAudioSource;
        private Coroutine musicFadeCoroutine;

        /// <summary>
        /// Changes the music audio clip with optional fade out and fade in delay.
        /// </summary>
        /// <param name="newClip">The new music AudioClip.</param>
        /// <param name="fadeDelay">Fade duration in seconds. If 0, switches instantly.</param>
        public void ChangeMusic(AudioClip newClip, float fadeDelay = 0f)
        {
            if (musicFadeCoroutine != null)
                StopCoroutine(musicFadeCoroutine);
            musicFadeCoroutine = StartCoroutine(FadeMusicCoroutine(newClip, fadeDelay));
        }

        private IEnumerator FadeMusicCoroutine(AudioClip newClip, float fadeDelay)
        {
            if (fadeDelay <= 0f)
            {
                musicAudioSource.clip = newClip;
                musicAudioSource.Play();
                yield break;
            }

            float startVolume = musicAudioSource.volume;
            // Fade out
            for (float t = 0; t < fadeDelay; t += Time.deltaTime)
            {
                musicAudioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDelay);
                yield return null;
            }
            musicAudioSource.volume = 0f;
            musicAudioSource.Stop();
            musicAudioSource.clip = newClip;
            musicAudioSource.Play();
            // Fade in
            for (float t = 0; t < fadeDelay; t += Time.deltaTime)
            {
                musicAudioSource.volume = Mathf.Lerp(0f, startVolume, t / fadeDelay);
                yield return null;
            }
            musicAudioSource.volume = startVolume;
        }
    }
}