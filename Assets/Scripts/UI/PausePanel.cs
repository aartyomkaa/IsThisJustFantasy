using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class PausePanel : MonoBehaviour
    {
        [SerializeField] private Button _restartSceneButton;
        [SerializeField] private Button _exitToMainMenuButton;
        [SerializeField] private GameObject _panel;

        private Pauser _currentPauser;

        public Button OpenButton;
        public Button CloseButton;
       
        public event Action MainMenuButtonClicked;
        public event Action RestartSceneButtonClicked;

        public void SignToPauserEvents(Pauser pauser)
        {
            _currentPauser = pauser;
            OpenButton.onClick.AddListener(OnOpenButtonClicked);
            CloseButton.onClick.AddListener(OnCloseButtonClicked);
            _exitToMainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _restartSceneButton.onClick.AddListener(OnRestartSceneButtonClicked);
        }

        private void OnDisable()
        {
            OpenButton.onClick.RemoveListener(OnOpenButtonClicked);
            CloseButton.onClick.RemoveListener(OnCloseButtonClicked);
            _exitToMainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _restartSceneButton.onClick.RemoveListener(OnRestartSceneButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            _currentPauser.Resume();
            MainMenuButtonClicked?.Invoke();
        }

        private void OnRestartSceneButtonClicked()
        {
            RestartSceneButtonClicked?.Invoke();
            _currentPauser.Resume();
        }

        private void OnOpenButtonClicked()
        {
            _panel.SetActive(true);
            _currentPauser.Pause();
        }

        private void OnCloseButtonClicked()
        {
            _panel.SetActive(false);
            _currentPauser.Resume();
        }
    }
}