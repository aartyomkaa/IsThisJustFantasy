using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts.Constants;
using Assets.Scripts.UI;

namespace Assets.Scripts.GameLogic
{
    internal class SceneLoader : MonoBehaviour
    {
        private PausePanel _currentPausePanel;

        private int _currentScene;
        private int _nextLevelNumber = 1;
        private NextLevelZone _currentNextLevelZone;

        private void OnEnable()
        {
            //LoadMenuScene();
        }

        private void OnDisable()
        {
            
            if (_currentPausePanel != null)
            {
                _currentPausePanel.MainMenuButtonClicked -= LoadMenuScene;
                _currentPausePanel.RestartSceneButtonClicked -= RestartCurrentScene;
            } 

            if(_currentNextLevelZone != null)
            {
                _currentNextLevelZone.LevelUped -= IncreaseLevel;
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

        private void RestartCurrentScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);          
        }

        private void IncreaseLevel()   // вызывать в момент первого показа панельки о прохождении уровня
        {
            _currentScene = SceneManager.GetActiveScene().buildIndex;

            Debug.Log("нынешний уровень вот такой - " + _currentScene);

            
            PlayerPrefs.SetInt(SceneNames.LastAvailableLevel, _currentScene += _nextLevelNumber);

            Debug.Log("Повысил уровень на такой - " + PlayerPrefs.GetInt(SceneNames.LastAvailableLevel));
        }

        public void SignToNextLevelPanelToZone(NextLevelZone nextLevelZone)
        {
            _currentNextLevelZone = nextLevelZone;
            _currentNextLevelZone.LevelUped += IncreaseLevel;
        }      
    }
}
