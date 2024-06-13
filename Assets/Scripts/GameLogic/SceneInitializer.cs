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
using System.Collections.Generic;
using System;

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
        [SerializeField] private BackgraoundPauser _backgroundPauser;
        [SerializeField] private InterstitialAdTimer _interstitialAdTimer;

       // private List<ColliderPanelEventer> _eventers;

        public event Action<ColliderPanelEventer> EventerWasAdded;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            _buildingSystem.EventerWithAdButtonWasMade += OnEventerWasMade;

            _enemyFactory.FinalWaveCleared += _nextLevelZone.OnAllWavesDefeated;
            _enemyBuilding.AdButton.onClick.AddListener(_interstitialAd.Show);

            _mainBuilding.BuildWithEventorWasMade += OnEventerWasMade;
            _mainBuilding.Destroyed += _score.OpenEndGamePanel;

            //_eventers.Add(_mainBuilding.EventerToSend);
            //_eventers.Add(_enemyBuilding.EventerToSend);

            EventerWasAdded += _interstitialAdTimer.AddEventer;
        }

        private void Start()
        {
            InitializeAdTimer();
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            _buildingSystem.EventerWithAdButtonWasMade -= OnEventerWasMade;

            _enemyFactory.FinalWaveCleared -= _nextLevelZone.OnAllWavesDefeated;
            _enemyFactory.WaveStarted -= _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged -= _globalUI.OnWaveSpawnAmountChanged;
            _enemyBuilding.AdButton.onClick.RemoveListener(_interstitialAd.Show);
            _mainBuilding.BuildWithEventorWasMade -= OnEventerWasMade;
            _mainBuilding.Destroyed -= _score.OpenEndGamePanel;
            EventerWasAdded -= _interstitialAdTimer.AddEventer;

        }

        private void OnEventerWasMade(ColliderPanelEventer currentEventer)
        {

            Debug.Log("я евентер, вот моё имя - " + currentEventer.name);
            EventerWasAdded?.Invoke(currentEventer);
            // button.onClick.AddListener(_videoAd.Show);
            //_interstitialAdTimer.AddEventer(currentEventer);
            _interstitialAdTimer.SignToAdButtons();

           

            if(_interstitialAdTimer.IsOnCooldown == true)
            {
                currentEventer.DeActivateSecondButton();
            }
           // currentEventer.AddButtonClicked +=
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

            DesktopInput input = Instantiate(_desktopInput, transform);
            input.Init(player);
        }

        private void InitializeUI(Player player)
        {
            Pauser pauser = new Pauser(_audioMixer, _mobileInput);

            // InitializeAd(pauser);

            _interstitialAd.Init(pauser);
            _videoAd.Init(pauser);
           // _interstitialAdTimer.Init(_interstitialAd);
            //_interstitialAdTimer.AddEventer(_mainBuilding.EventerToSend);
           // _interstitialAdTimer.AddEventer(_enemyBuilding.EventerToSend);
           // _interstitialAdTimer.SignToAdButtons();


            _globalUI.Init(player,_sceneLoader, _audioMixer, pauser, _videoAd, _interstitialAd); 
            _score.Init(player, pauser, _sceneLoader, _globalUI.EndGamePanel);
            _nextLevelZone.Init(_score, _sceneLoader, player, pauser, _globalUI.NextLevelPanel, _globalUI.WinGamePanel); 
            _enemyFactory.WaveStarted += _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged += _globalUI.OnWaveSpawnAmountChanged;

            

            _backgroundPauser.Init(pauser, _audioMixer);
        }

        private void InitializeAdTimer()
        {
            _interstitialAdTimer.Init(_interstitialAd);
            //_interstitialAdTimer.AddEventer(_mainBuilding.EventerToSend);   
            //_interstitialAdTimer.AddEventer(_enemyBuilding.EventerToSend);   
           // _interstitialAdTimer.SignToAdButtons();  
        }
    }
}