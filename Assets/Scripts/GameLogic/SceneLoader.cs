using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Constants;
using Assets.Scripts.UI;

namespace Assets.Scripts.GameLogic
{
    internal class SceneLoader : MonoBehaviour
    {
        private PausePanel _currentPausePanel;

        private int _totalScenes;
        private int _oneLevelIndex = 1;
        private bool _isLastLevelReached;
        private int _secondLevelIndex = 3;

        public event Action<bool> LastLevelReached;

        private void Start()
        {
            _totalScenes = SceneManager.sceneCountInBuildSettings;

            if (SceneManager.GetActiveScene().buildIndex + _oneLevelIndex == _totalScenes)
            {
                _isLastLevelReached = true;
                LastLevelReached?.Invoke(_isLastLevelReached);
            }
            else if (SceneManager.GetActiveScene().buildIndex == _secondLevelIndex)
            {
                PlayerPrefs.SetInt(PlayerConfigs.HasPassedTutorial, 1);
            }
        }

        private void OnDisable()
        {
            if (_currentPausePanel != null)
            {
                _currentPausePanel.MainMenuButtonClicked -= LoadMenuScene;
                _currentPausePanel.RestartSceneButtonClicked -= RestartCurrentScene;
            }
        }

        public void SignToPausePanelEvents(PausePanel pausePanel)
        {
            _currentPausePanel = pausePanel;
            _currentPausePanel.MainMenuButtonClicked += LoadMenuScene;
            _currentPausePanel.RestartSceneButtonClicked += RestartCurrentScene;
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadSceneAsync(SceneNames.Menu, LoadSceneMode.Single);
        }

        public void LoadNextScene()
        {
            if (SceneManager.GetActiveScene().buildIndex + _oneLevelIndex <= _totalScenes)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + _oneLevelIndex, LoadSceneMode.Single); 
            }  
        }

        public void RestartCurrentScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);          
        }    
    }
}
