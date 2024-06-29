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
        private bool _isPaused;

        public Button OpenButton;
        public Button CloseButton;
     
        public event Action MainMenuButtonClicked;
        public event Action RestartSceneButtonClicked;
        public event Action<bool> ChangedPauseStatus;


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
            _pauser.Resume();
            _isPaused = false;
            ChangedPauseStatus?.Invoke(_isPaused);
            MainMenuButtonClicked?.Invoke();
        }

        private void OnRestartSceneButtonClicked()
        {
            RestartSceneButtonClicked?.Invoke();
            _pauser.Resume();
            _isPaused = false;
            ChangedPauseStatus?.Invoke(_isPaused);
        }

        private void OnOpenButtonClicked()
        {   
            _panel.SetActive(true);
            _pauser.Pause();
            _isPaused = true;
            ChangedPauseStatus?.Invoke(_isPaused);
        }

        private void OnCloseButtonClicked()
        {
            _panel.SetActive(false);
            _pauser.Resume();
            _isPaused = false;
            ChangedPauseStatus?.Invoke(_isPaused);
        }
    }
}