using System;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Overcrowded
{
    public class SelectLevel : MonoBehaviour
    {
        [SerializeField] private int levelNumber;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text levelNumberText;
        [SerializeField] private GameObject locked;
        [SerializeField] private string _buttonPrefix = "Level ";

        [Inject] private UserState userState;
        private const string LevelSceneName = "Level_";

        public bool Locked => levelNumber > userState.Level;

        private void Awake()
        {
            locked.SetActive(Locked);
            
            levelNumberText.text = $"{_buttonPrefix} {levelNumber}";
            button.onClick.AddListener(LoadLevelScene);
        }

        private void LoadLevelScene()
        {
            if (Locked)
                return;
            
            SceneManager.LoadScene($"{LevelSceneName}{levelNumber}");
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(LoadLevelScene);
        }
    }
}