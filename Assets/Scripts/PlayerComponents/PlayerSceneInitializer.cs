using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.WebUtility;
using Assets.Scripts.Audio;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.BuildingSystem.System;
using Assets.Scripts.CameraComponents;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.GameLogic;
using Assets.Scripts.PlayerInput;
using Assets.Scripts.YandexSDK;
using Assets.Scripts.UI;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerSceneInitializer : MonoBehaviour
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
        [SerializeField] private Score _score;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            _buildingSystem.EventerWithAdButtonWasMade += OnEventerWithAdButtonWasMade;

            _enemyFactory.FinalWaveCleared += _nextLevelZone.OnAllWavesDefeated;

            _mainBuilding.Destroyed += _score.OpenEndGamePanel;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            _buildingSystem.EventerWithAdButtonWasMade -= OnEventerWithAdButtonWasMade;

            _enemyFactory.FinalWaveCleared -= _nextLevelZone.OnAllWavesDefeated;
            _enemyFactory.WaveStarted -= _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged -= _globalUI.OnWaveSpawnAmountChanged;

            _mainBuilding.Destroyed -= _score.OpenEndGamePanel;
        }

        private void OnEventerWithAdButtonWasMade(Button button)
        {
            Debug.Log(button.name); // ?????
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
            Pauser pauser = new Pauser(_audioMixer, _mobileInput);

            _score.Init(player, pauser, _sceneLoader);
            _nextLevelZone.Init(_score, _sceneLoader, player);
            _globalUI.Init(player, _sceneLoader, _audioMixer);
            _enemyFactory.WaveStarted += _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged += _globalUI.OnWaveSpawnAmountChanged;
        }
    }
}