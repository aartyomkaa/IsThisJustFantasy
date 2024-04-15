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

        private Pauser _pauser;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;  
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Player player = InitializePlayer();

            InitializeInput(player);
            InitializeUI(player);
            InitializeSound(_globalUI.SoundToggler);
            _pauser = new Pauser(_audioMixer, _mobileInput);
            _globalUI.PausePanel.SignToPauserEvents(_pauser);
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
//#if UNITY_WEBGL && !UNITY_EDITOR
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
//#endif
        }

        private void InitializeUI(Player player)
        {
            _globalUI.PlayerUI.SignToPlayersValuesChanges(player.GetComponent<PlayerHealth>(), player.Wallet);
        }
    }
}
