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

        [Inject] private UserState userState;
        private const string LevelSceneName = "Level_";

        private void Awake()
        {
            if (userState.Level < levelNumber)
                locked.SetActive(true);
            
            levelNumberText.text = levelNumber.ToString();
            button.onClick.AddListener(LoadLevelScene);
        }

        private void LoadLevelScene()
        {
            SceneManager.LoadScene($"{LevelSceneName}{levelNumber}");
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(LoadLevelScene);
        }
    }
}