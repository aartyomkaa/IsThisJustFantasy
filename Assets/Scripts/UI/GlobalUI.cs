<<<<<<< HEAD
using System;
using UnityEngine;
using TMPro;
using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic;
using Assets.Scripts.PlayerComponents;
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Audio;
using UnityEngine.UI;
using Assets.Scripts.GameLogic;
using TMPro;

>>>>>>> parent of 7089f0a4 (scorePanel)

namespace Assets.Scripts.UI
{
    internal class GlobalUI : MonoBehaviour
    {
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private SoundToggler _soundToggler;
<<<<<<< HEAD
        [SerializeField] private GameObject _waves;
        [SerializeField] private TMP_Text _wavesNumber;

        private SceneLoader _sceneLoader;
        private AudioMixer _audioMixer;
        private Player _player;

        private void OnDisable()
        {
            _audioMixer.VolumeValueChanged -= _soundToggler.SetCurrentStatus;

            _player.LevelChanged -= _playerUI.OnLevelChanged;
=======
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
>>>>>>> parent of 7089f0a4 (scorePanel)
        }

        public void Init(Player player, SceneLoader loader, AudioMixer mixer)
        {
<<<<<<< HEAD
            _sceneLoader = loader;
            _player = player;
            _audioMixer = mixer;

            _player.LevelChanged += _playerUI.OnLevelChanged;
            _playerUI.SignToPlayerValuesChanges(player);

            _audioMixer.SignSoundValuesChanges(_soundToggler);
            _audioMixer.VolumeValueChanged += _soundToggler.SetCurrentStatus;

            _sceneLoader.SignToPausePanelEvents(_pausePanel);
=======
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
>>>>>>> parent of 7089f0a4 (scorePanel)
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