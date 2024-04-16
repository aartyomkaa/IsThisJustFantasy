using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts.Constants;
using Assets.Scripts.UI;

namespace Assets.Scripts.GameLogic
{
    internal class SceneLoader : MonoBehaviour
    {
        private PausePanel _currentPausePanel;

        private void OnEnable()
        {
            //LoadMenuScene();
        }

        private void OnDisable()
        {
            _currentPausePanel.MainMenuButtonClicked -= LoadMenuScene;
            _currentPausePanel.RestartSceneButtonClicked -= RestartCurrentScene;
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
    }
}
