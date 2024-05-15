using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Audio;
using UnityEngine.UI;
using Assets.Scripts.GameLogic;
using TMPro;


namespace Assets.Scripts.UI
{
    internal class GlobalUI : MonoBehaviour
    {
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private SoundToggler _soundToggler;
        [SerializeField] private NextLevelPanel _nextLevelPanel;
        [SerializeField] private TMP_Text _waves;
        //[SerializeField] private SceneLoader _sceneLoader;

        private NextLevelZone _currentNextLevelZone;
        private AudioMixer _currentaudioMixer;

        public PlayerUI PlayerUI => _playerUI;
        public PausePanel PausePanel => _pausePanel;
        public SoundToggler SoundToggler => _soundToggler;


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

        public void OnWaveStarted(int amount)
        {
            _waves.text = amount.ToString();
            _waves.gameObject.SetActive(true);
        }

        public void OnWaveSpawnAmountChanged(int amount)
        {
            if(amount > 0) 
            {
                _waves.text = amount.ToString();
            }
            else
            {
                _waves.gameObject.SetActive(false);
            }
        }
    }
}