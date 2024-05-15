﻿using Assets.Scripts.CameraComponents;
using Assets.Scripts.PlayerInput;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Agava.WebUtility;
using Assets.Scripts.UI;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.Audio;
using Assets.Scripts.YandexSDK;
using Assets.Scripts.GameLogic;
using Assets.Scripts.BuildingSystem.System;
using UnityEngine.UI;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerSceneInitializer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TargetFollower _targetFollower;
        [SerializeField] private DesktopInput _desktopInput;
        [SerializeField] private MobileInput _mobileInput;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private GlobalUI _globalUI;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private InterstitialAdShower _interstitialAd;
        [SerializeField] private VideoADShower _videoAd;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private BuildingService _buildingSystem;
        [SerializeField] private NextLevelZone _nextLevelZone;

        private Pauser _pauser;
        private Player _currentPlayer;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            _buildingSystem.EventerWithAdButtonWasMade += OnEventerWithAdButtonWasMade;
            _enemyFactory.FinalWaveCleared += _nextLevelZone.OnAllWavesDefeated;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _buildingSystem.EventerWithAdButtonWasMade -= OnEventerWithAdButtonWasMade;
            _enemyFactory.FinalWaveCleared -= _nextLevelZone.OnAllWavesDefeated;
            _currentPlayer.LevelChanged -= _globalUI.PlayerUI.OnLevelChanged;
            _enemyFactory.WaveStarted -= _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged -= _globalUI.OnWaveSpawnAmountChanged;
        }

        private void OnEventerWithAdButtonWasMade(Button button)
        {
            Debug.Log(button.name);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Player player = InitializePlayer();  //тут изменил на _currentPlayer, чтобы подписываться на изменения уровня и отписываться

            _currentPlayer = InitializePlayer();

            InitializeInput(_currentPlayer);
            InitializeUI(_currentPlayer);
            InitializeSound(_globalUI.SoundToggler);
            _currentPlayer.LevelChanged += _globalUI.PlayerUI.OnLevelChanged;

        }

        private Player InitializePlayer()
        {
            Player player = Instantiate(_player, transform.position, Quaternion.identity);
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

            _targetFollower.Init(player.transform);
            agent.Warp(transform.position);

            return player;
        }

        private void InitializeSound(SoundToggler soundToggler) 
        {
            _audioMixer.SignSoundValuesChanges(soundToggler);
        }

        private void InitializeInput(Player player)
        {
//#if UNITY_WEBGL && !UNITY_EDITOR
            if (1 == 2)
            {
                MobileInput input = Instantiate(_mobileInput, transform);
                input.Init(player);
            }
            else
            {
                DesktopInput input = Instantiate(_desktopInput, transform);
                input.Init(player);
            }
//#endif
        }

        private void InitializeUI(Player player)
        {
            _pauser = new Pauser(_audioMixer, _mobileInput);
           
            _globalUI.PlayerUI.SignToPlayersValuesChanges(player.GetComponent<PlayerHealth>(), player.Wallet, player.CurrentLevel);
            _globalUI.PausePanel.SignToPauserEvents(_pauser);
            _globalUI.SignSoundTogglerToAudio(_audioMixer);
            _globalUI.SignToNextLevelPanelToZone(_nextLevelZone);
            _sceneLoader.SignToPausePanelEvents(_globalUI.PausePanel);
            _sceneLoader.SignToNextLevelPanelToZone(_nextLevelZone);
            _enemyFactory.WaveStarted += _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged += _globalUI.OnWaveSpawnAmountChanged;
        }
    }
}
