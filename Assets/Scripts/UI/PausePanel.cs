using Assets.Scripts.GameLogic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class PausePanel : MonoBehaviour
    {
        [SerializeField] public Button _openButton;
        [SerializeField] public Button _closeButton;
        [SerializeField] private Button _restartSceneButton;
        [SerializeField] private Button _exitToMainMenuButton;
        [SerializeField] private GameObject _panel;

        private  Pauser _currentPauser;

        public event Action MainMenuButtonClicked;
        public event Action RestartSceneButtonClicked;


        public void SignToPauserEvents(Pauser pauser)
        {
            _currentPauser = pauser;
            _openButton.onClick.AddListener(OnOpenButtonClicked);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _exitToMainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _restartSceneButton.onClick.AddListener(OnRestartSceneButtonClicked);
        }


        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClicked);
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _exitToMainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _restartSceneButton.onClick.RemoveListener(OnRestartSceneButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
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