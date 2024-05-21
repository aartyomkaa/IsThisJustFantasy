using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Audio;
using UnityEngine.UI;
using Assets.Scripts.GameLogic;
using TMPro;
using System;
using Assets.Scripts.PlayerComponents;

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

        private SceneLoader _sceneLoader;
        private NextLevelZone _nextLevelZone;
        private AudioMixer _audioMixer;
        private Score _score;
        private Pauser _pauser;
        private Player _player;

        public event Action NextLevelButtonClicked;

        private void OnDisable()
        {
            _nextLevelZone.PlayerWentIn -= OpenNextLevelPanel;
            _nextLevelPanel.BackButtonClicked -= CloseNextLevelPanel;
            _nextLevelPanel.ContinueButtonClicked -= OnNextLevelButtonClicked;
            _audioMixer.VolumeValueChanged -= _soundToggler.SetCurrentStatus;

            _player.LevelChanged -= _playerUI.OnLevelChanged;
        }

        public void Init(Player player, NextLevelZone zone, Score score, Pauser pauser, SceneLoader loader, AudioMixer mixer)
        {
            _pauser = pauser;
            _score = score;
            _sceneLoader = loader;
            _nextLevelZone = zone;
            _player = player;
            _audioMixer = mixer;

            _player.LevelChanged += _playerUI.OnLevelChanged;
            _playerUI.SignToPlayerValuesChanges(player);

            _audioMixer.SignSoundValuesChanges(_soundToggler);
            _audioMixer.VolumeValueChanged += _soundToggler.SetCurrentStatus;

            _sceneLoader.SignToPausePanelEvents(_pausePanel);
            _sceneLoader.SignToNextLevelPanelToZone(_nextLevelZone);

            // _currentNextLevelZone.PlayerWentOut += CloseNextLevelPanel; ?????????
            _nextLevelZone.PlayerWentIn += OpenNextLevelPanel;
            _nextLevelPanel.BackButtonClicked += CloseNextLevelPanel;
            _nextLevelPanel.ContinueButtonClicked += OnNextLevelButtonClicked;
        }

        public void OnWaveStarted(int amount)
        {
            _waves.text = amount.ToString();
            _waves.gameObject.SetActive(true);
        }

        public void OnWaveSpawnAmountChanged(int amount)
        {
            if (amount > 0)
            {
                _waves.text = amount.ToString();
            }
            else
            {
                _waves.gameObject.SetActive(false);
            }
        }

        private void OpenNextLevelPanel()
        {
            _nextLevelPanel.gameObject.SetActive(true);
            _score.UpdateLevelScore();
            _nextLevelPanel.SetTextScore(_score.LevelScore.ToString());
            _pauser.Pause();
        }

        private void CloseNextLevelPanel()
        {
            _pauser.Resume();
            _nextLevelPanel.gameObject.SetActive(false);
        }

        private void OnNextLevelButtonClicked()
        {
            _pauser.Resume();
            _score.UpdateTotalScore();
            NextLevelButtonClicked?.Invoke();
        }
    }
}