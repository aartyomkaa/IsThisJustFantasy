using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Audio;
using UnityEngine.UI;
using Assets.Scripts.GameLogic;


namespace Assets.Scripts.UI
{
    internal class GlobalUI : MonoBehaviour
    {
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private SoundToggler _soundToggler;
        [SerializeField] private NextLevelPanel _nextLevelPanel;
        [SerializeField] private Button _goToMenu;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private NextLevelZone _currentNextLevelZone;

        private AudioMixer _currentaudioMixer;

        public PlayerUI PlayerUI => _playerUI;
        public PausePanel PausePanel => _pausePanel;
        public SoundToggler SoundToggler => _soundToggler;


        private void OnEnable()
        {
           // _goToMenu.onClick.AddListener(_sceneLoader.LoadMenuScene);
        }


        public void SignToNextLevelPanelToZone(NextLevelZone nextLevelZone)
        {
            _currentNextLevelZone = nextLevelZone;
            _currentNextLevelZone.PlayerWentIn += OpenNextLevelPanel;
            _currentNextLevelZone.PlayerWentOut += CloseNextLevelPanel;
        }

        private void OnDisable()
        {
            _currentNextLevelZone.PlayerWentIn -= OpenNextLevelPanel;
            _currentNextLevelZone.PlayerWentOut -= CloseNextLevelPanel;
            _currentaudioMixer.VolumeValueChanged -= _soundToggler.SetCurrentStatus;
           // _goToMenu.onClick.RemoveListener(_sceneLoader.LoadMenuScene);
        }

        public void SignSoundTogglerToAudio(AudioMixer audioMixer)
        {
            _currentaudioMixer = audioMixer;
            _currentaudioMixer.VolumeValueChanged += _soundToggler.SetCurrentStatus;
        }

        private void OpenNextLevelPanel()
        {
            _nextLevelPanel.gameObject.SetActive(true);
        }

        private void CloseNextLevelPanel()
        {
            _nextLevelPanel.gameObject.SetActive(false);
        }
    }
}