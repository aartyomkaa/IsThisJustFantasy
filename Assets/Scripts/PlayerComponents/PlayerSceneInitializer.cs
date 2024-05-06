using Assets.Scripts.CameraComponents;
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

        private Pauser _pauser;
        private NextLevelZone _nextLevelZone;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            _buildingSystem.EventerWithAdButtonWasMade += OnEventerWithAdButtonWasMade;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _buildingSystem.EventerWithAdButtonWasMade -= OnEventerWithAdButtonWasMade;
        }

        private void OnEventerWithAdButtonWasMade(Button button)
        {
            Debug.Log(button.name);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Player player = InitializePlayer();
            _nextLevelZone = GetComponentInChildren<NextLevelZone>();

            InitializeInput(player);
            InitializeUI(player);
            InitializeSound(_globalUI.SoundToggler);
        }

        private Player InitializePlayer()
        {
            Player player = Instantiate(_player);
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
            _pauser = new Pauser(_audioMixer, _mobileInput);
            _globalUI.PlayerUI.SignToPlayersValuesChanges(player.GetComponent<PlayerHealth>(), player.Wallet);
            _globalUI.PausePanel.SignToPauserEvents(_pauser);
            _globalUI.SignSoundTogglerToAudio(_audioMixer);
            _globalUI.SignToNextLevelPanelToZone(_nextLevelZone);
            _sceneLoader.SignToPausePanelEvents(_globalUI.PausePanel);
            _sceneLoader.SignToNextLevelPanelToZone(_nextLevelZone);
        }
    }
}
