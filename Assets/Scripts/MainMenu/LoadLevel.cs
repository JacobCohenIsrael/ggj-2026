using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overcrowded
{
    public class LoadLevel : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void Load()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}