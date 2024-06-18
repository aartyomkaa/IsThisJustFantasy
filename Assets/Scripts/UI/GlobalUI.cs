using System;
using UnityEngine;
using TMPro;
using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.YandexSDK;
using Agava.YandexGames;

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
        private VideoAdShower _videoAdShower;


        public ScorePanel NextLevelPanel => _nextLevelPanel;
        public ScorePanel EndGamePanel => _endGamePanel;
        public ScorePanel WinGamePanel => _winGamePanel;

        private void OnDisable()
        {
            _audioMixer.VolumeValueChanged -= _soundToggler.SetCurrentStatus;

            _player.LevelChanged -= _playerUI.OnLevelChanged;

            _nextLevelPanel.ContinueButtonPressed -= _videoAdShower.Show;
            _endGamePanel.ContinueButtonPressed -= _videoAdShower.Show;
            _endGamePanel.BackButtonPressed -= _videoAdShower.Show;
            _winGamePanel.BackButtonPressed -= _videoAdShower.Show;
        }

        public void Init(Player player, SceneLoader loader, AudioMixer mixer, Pauser pauser, VideoAdShower videoAdShower) 
        {
            _sceneLoader = loader;
            _player = player;
            _audioMixer = mixer;
            _videoAdShower = videoAdShower;

            _player.LevelChanged += _playerUI.OnLevelChanged;
            _playerUI.SignToPlayerValuesChanges(player);
            _pausePanel.Init(pauser);

            _audioMixer.SignSoundValuesChanges(_soundToggler);
            _audioMixer.VolumeValueChanged += _soundToggler.SetCurrentStatus;

            _sceneLoader.SignToPausePanelEvents(_pausePanel);

            _nextLevelPanel.ContinueButtonPressed += _videoAdShower.Show;
            _endGamePanel.ContinueButtonPressed += _videoAdShower.Show;
            _endGamePanel.BackButtonPressed += _videoAdShower.Show;
            _winGamePanel.BackButtonPressed += _videoAdShower.Show;
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