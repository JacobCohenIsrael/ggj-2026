using DG.Tweening;
using Overcrowded.Game.UI.MainMenu;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overcrowded.Services
{
    public class LevelLoader
    {
        [Inject] private DarkOverlayController _darkOverlay;

        public void LoadLevel(int level, DarkOverlayController.FadeParams fadeIn, AnimationCurve curve = null)
        {
            _darkOverlay.CreateFadeInTween(fadeIn)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene($"Level_{level}", LoadSceneMode.Single);
                    DoPostLevelLoad();
                });
        }

        public void LoadLevel(string sceneName, DarkOverlayController.FadeParams fadeIn, AnimationCurve curve = null)
        {
            _darkOverlay.CreateFadeInTween(fadeIn)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                    DoPostLevelLoad();
                });
        }

        private void DoPostLevelLoad()
        {
            //nothing
        }
    }
}