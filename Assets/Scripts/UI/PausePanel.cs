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

        private Pauser _pauser;

        public bool IsOpen => _panel.activeSelf;

        public Button OpenButton;
        public Button CloseButton;
     
        public event Action MainMenuButtonClicked;
        public event Action RestartSceneButtonClicked;

        private void OnEnable()
        {
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

        public void Init(Pauser pauser)
        {
            _pauser = pauser;
        }

        private void OnMainMenuButtonClicked()
        {
            ClosePanel();
            MainMenuButtonClicked?.Invoke();
        }

        private void OnRestartSceneButtonClicked()
        {
            ClosePanel();
            RestartSceneButtonClicked?.Invoke();
        }

        private void OnOpenButtonClicked()
        {   
            _panel.SetActive(true);
            _pauser.Pause();
        }

        private void OnCloseButtonClicked()
        {
            ClosePanel();
        }

        private void ClosePanel()
        {
            _panel.SetActive(false);
            _pauser.Resume();
        }
    }
}