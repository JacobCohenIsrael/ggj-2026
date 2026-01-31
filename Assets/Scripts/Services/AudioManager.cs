using System.Collections;
using UnityEngine;

namespace Overcrowded.Services
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;
        private Coroutine musicFadeCoroutine;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        /// <summary>
        /// Plays a SFX AudioClip using the sfxAudioSource. Stops any previous SFX.
        /// </summary>
        /// <param name="clip">The SFX AudioClip to play.</param>
        /// <param name="volume">Volume (0-1), default 1.</param>
        public void PlaySfx(AudioClip clip, float volume = 1f)
        {
            if (clip == null || sfxAudioSource == null) return;
            sfxAudioSource.Stop();
            sfxAudioSource.clip = clip;
            sfxAudioSource.volume = volume;
            sfxAudioSource.loop = false;
            sfxAudioSource.Play();
        }

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

        /// <summary>
        /// Changes the music to an entrance (intro) track, then loops a second track, with optional fade.
        /// </summary>
        /// <param name="entranceClip">The intro AudioClip (played once, can be null).</param>
        /// <param name="loopClip">The loopable AudioClip (will loop, required).</param>
        /// <param name="fadeDelay">Fade duration in seconds. If 0, switches instantly.</param>
        public void ChangeMusicWithIntro(AudioClip entranceClip, AudioClip loopClip, float fadeDelay = 0f)
        {
            if (musicFadeCoroutine != null)
                StopCoroutine(musicFadeCoroutine);
            musicFadeCoroutine = StartCoroutine(FadeMusicWithIntroCoroutine(entranceClip, loopClip, fadeDelay));
        }

        private IEnumerator FadeMusicWithIntroCoroutine(AudioClip entranceClip, AudioClip loopClip, float fadeDelay)
        {
            if (loopClip == null) yield break;
            float startVolume = musicAudioSource.volume;
            if (fadeDelay > 0f)
            {
                // Fade out
                for (float t = 0; t < fadeDelay; t += Time.deltaTime)
                {
                    musicAudioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDelay);
                    yield return null;
                }
                musicAudioSource.volume = 0f;
            }
            musicAudioSource.Stop();
            // Play entrance (intro) clip if provided
            if (entranceClip != null)
            {
                musicAudioSource.clip = entranceClip;
                musicAudioSource.loop = false;
                musicAudioSource.Play();
                // Fade in
                if (fadeDelay > 0f)
                {
                    for (float t = 0; t < fadeDelay; t += Time.deltaTime)
                    {
                        musicAudioSource.volume = Mathf.Lerp(0f, startVolume, t / fadeDelay);
                        yield return null;
                    }
                    musicAudioSource.volume = startVolume;
                }
                // Wait for entrance clip to finish
                while (musicAudioSource.isPlaying)
                    yield return null;
            }
            // Play loopable clip
            musicAudioSource.clip = loopClip;
            musicAudioSource.loop = true;
            musicAudioSource.Play();
            if (fadeDelay > 0f && entranceClip == null)
            {
                // If no intro, fade in loop
                for (float t = 0; t < fadeDelay; t += Time.deltaTime)
                {
                    musicAudioSource.volume = Mathf.Lerp(0f, startVolume, t / fadeDelay);
                    yield return null;
                }
                musicAudioSource.volume = startVolume;
            }
        }
    }
}