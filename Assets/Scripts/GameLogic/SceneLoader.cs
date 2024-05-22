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
        private int _firstLevelIndex = 2;

        private void Start()
        {
            _totalScenes = SceneManager.sceneCountInBuildSettings;
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
            if (SceneManager.GetActiveScene().buildIndex + _firstLevelIndex <= _totalScenes)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + _firstLevelIndex, LoadSceneMode.Single);
            }
        }

        public void RestartCurrentScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);          
        }    
    }
}
