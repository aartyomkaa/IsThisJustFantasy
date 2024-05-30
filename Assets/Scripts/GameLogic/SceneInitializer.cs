using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.WebUtility;
using Assets.Scripts.Audio;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.BuildingSystem.System;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.CameraComponents;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.PlayerInput;
using Assets.Scripts.YandexSDK;
using Assets.Scripts.UI;

namespace Assets.Scripts.GameLogic
{
    internal class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private MainBuilding _mainBuilding;
        [SerializeField] private TargetFollower _targetFollower;
        [SerializeField] private DesktopInput _desktopInput;
        [SerializeField] private MobileInput _mobileInput;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private GlobalUI _globalUI;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private BuildingService _buildingSystem;
        [SerializeField] private NextLevelZone _nextLevelZone;
        [SerializeField] private InterstitialAdShower _interstitialAd;
        [SerializeField] private VideoAdShower _videoAd;
        [SerializeField] private Score _score;
        [SerializeField] private EnemyBuilding _enemyBuilding;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            _buildingSystem.EventerWithAdButtonWasMade += OnEventerWithAdButtonWasMade;

            _enemyFactory.FinalWaveCleared += _nextLevelZone.OnAllWavesDefeated;
            _enemyBuilding.AdButton.onClick.AddListener(_interstitialAd.Show);

            _mainBuilding.Destroyed += _score.OpenEndGamePanel;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            _buildingSystem.EventerWithAdButtonWasMade -= OnEventerWithAdButtonWasMade;

            _enemyFactory.FinalWaveCleared -= _nextLevelZone.OnAllWavesDefeated;
            _enemyFactory.WaveStarted -= _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged -= _globalUI.OnWaveSpawnAmountChanged;
            _enemyBuilding.AdButton.onClick.RemoveListener(_interstitialAd.Show);

            _mainBuilding.Destroyed -= _score.OpenEndGamePanel;
        }

        private void OnEventerWithAdButtonWasMade(Button button)
        {
            button.onClick.AddListener(_interstitialAd.Show);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Player player = InitializePlayer();

            InitializeInput(player);
            InitializeUI(player);
        }

        private Player InitializePlayer()
        {
            Player player = Instantiate(_player, transform.position, Quaternion.identity);
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

            _targetFollower.Init(player.transform);
            agent.Warp(transform.position);

            return player;
        }

        private void InitializeInput(Player player)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (Device.IsMobile)
            {
                MobileInput input = Instantiate(_mobileInput, transform);
                input.Init(player);
            }
            else
            {
                DesktopInput input = Instantiate(_desktopInput, transform);
                input.Init(player);
            }
#endif
        }

        private void InitializeUI(Player player)
        {
            Pauser pauser = new Pauser(_audioMixer, _mobileInput);
       
            _globalUI.Init(player,_sceneLoader, _audioMixer, pauser, _videoAd); 
            _score.Init(player, pauser, _sceneLoader, _globalUI.EndGamePanel);
            _nextLevelZone.Init(_score, _sceneLoader, player, pauser, _globalUI.NextLevelPanel, _globalUI.WinGamePanel); 
            _enemyFactory.WaveStarted += _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged += _globalUI.OnWaveSpawnAmountChanged;

            _interstitialAd.Init(pauser);
            _videoAd.Init(pauser);
        }
    }
}