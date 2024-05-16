using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Audio;
using UnityEngine.UI;
using Assets.Scripts.GameLogic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using System;


namespace Assets.Scripts.UI
{
    internal class GlobalUI : MonoBehaviour
    {
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private SoundToggler _soundToggler;
        [SerializeField] private ScorePanel _nextLevelPanel;
        [SerializeField] private ScorePanel _endGamePanel;
        [SerializeField] private TMP_Text _waves;
        //[SerializeField] private SceneLoader _sceneLoader;

        private NextLevelZone _currentNextLevelZone;
        private AudioMixer _currentAudioMixer;
        private Score _currentPlayerScore;
        private Pauser _currentPauser;

        public PlayerUI PlayerUI => _playerUI;
        public PausePanel PausePanel => _pausePanel;
        public SoundToggler SoundToggler => _soundToggler;

        public event Action NextLevelButtonClicked;

        public void SignToNextLevelPanelToZone(NextLevelZone nextLevelZone, Score playerScore, Pauser _pauser)
        {
            _currentPauser = _pauser;
            _currentPlayerScore = playerScore;

            _currentNextLevelZone = nextLevelZone;
            _currentNextLevelZone.PlayerWentIn += OpenNextLevelPanel;
            // _currentNextLevelZone.PlayerWentOut += CloseNextLevelPanel;
            _nextLevelPanel.BackButtonClicked += CloseNextLevelPanel;
            _nextLevelPanel.ContinueButtonClicked += OnNextLevelButtonClicked;
        }

        private void OnDisable()
        {
            _currentNextLevelZone.PlayerWentIn -= OpenNextLevelPanel;
             _nextLevelPanel.BackButtonClicked -= CloseNextLevelPanel;
            _currentAudioMixer.VolumeValueChanged -= _soundToggler.SetCurrentStatus;
            _nextLevelPanel.ContinueButtonClicked -= OnNextLevelButtonClicked;
        }

        public void SignSoundTogglerToAudio(AudioMixer audioMixer)
        {
            _currentAudioMixer = audioMixer;
            _currentAudioMixer.VolumeValueChanged += _soundToggler.SetCurrentStatus;
        }

        private void OpenNextLevelPanel()
        {
            _nextLevelPanel.gameObject.SetActive(true);
            _nextLevelPanel.SetTextScore(_currentPlayerScore);
            _currentPauser.Pause();

        }

        private void CloseNextLevelPanel()
        {
            _currentPauser.Resume();
            _nextLevelPanel.gameObject.SetActive(false);
        }

        private void OnNextLevelButtonClicked()
        {
            _currentPauser.Resume();
            NextLevelButtonClicked?.Invoke();
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