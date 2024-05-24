using System;
using UnityEngine;
using TMPro;
using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic;
using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.UI
{
    internal class GlobalUI : MonoBehaviour
    {
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private SoundToggler _soundToggler;
        [SerializeField] private GameObject _waves;
        [SerializeField] private TMP_Text _wavesNumber;
        [SerializeField] private ScorePanel _nextLevelPanel;
        [SerializeField] private ScorePanel _endGamePanel;
        [SerializeField] private ScorePanel _winGamePanel;


        private SceneLoader _sceneLoader;
        private AudioMixer _audioMixer;
        private Player _player;
        

        public ScorePanel NextLevelPanel => _nextLevelPanel;
        public ScorePanel EndGamePanel => _endGamePanel;

        private void OnDisable()
        {
            _audioMixer.VolumeValueChanged -= _soundToggler.SetCurrentStatus;

            _player.LevelChanged -= _playerUI.OnLevelChanged;
        }

        public void Init(Player player, SceneLoader loader, AudioMixer mixer, Pauser pauser) 
        {
            _sceneLoader = loader;
            _player = player;
            _audioMixer = mixer;

            _player.LevelChanged += _playerUI.OnLevelChanged;
            _playerUI.SignToPlayerValuesChanges(player);
            _pausePanel.SignToPauserEvents(pauser);

            _audioMixer.SignSoundValuesChanges(_soundToggler);
            _audioMixer.VolumeValueChanged += _soundToggler.SetCurrentStatus;

            _sceneLoader.SignToPausePanelEvents(_pausePanel);
        }

        public void OnWaveStarted(int amount)
        {
            _wavesNumber.text = amount.ToString();
            _waves.gameObject.SetActive(true);
        }

        public void OnWaveSpawnAmountChanged(int amount)
        {
            if (amount > 0)
            {
                _wavesNumber.text = amount.ToString();
            }
            else
            {
                _waves.gameObject.SetActive(false);
            }
        }
    }
}