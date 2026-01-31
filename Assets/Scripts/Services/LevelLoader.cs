using Overcrowded.Game.UI.MainMenu;
using Reflex.Attributes;
using UnityEngine.SceneManagement;

namespace Overcrowded.Services
{
    public class LevelLoader
    {
        [Inject] private DarkOverlayController _darkOverlay;

        public void LoadLevel(int level)
        {
            SceneManager.LoadScene($"Level_{level}", LoadSceneMode.Single);
            DoPostLevelLoad();
        }

        public void LoadLevel(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            DoPostLevelLoad();
        }

        private void DoPostLevelLoad()
        {
            //nothing
        }
    }
}