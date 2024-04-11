using Assets.Scripts.CameraComponents;
using Assets.Scripts.PlayerInput;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameLogic.Interfaces;
using Agava.WebUtility;
using System;
using Assets.Scripts.UI;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerSceneInitializer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TargetFollower _targetFollower;
        [SerializeField] private DesktopInput _desktopInput;
        [SerializeField] private MobileInput _mobileInput;
        [SerializeField] private PlayerUI _playerUI;

        private PlayerHealth _currentPlayerHealth;
        private PlayerWallet _currentPlayerWallet;


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
            Player player = Instantiate(_player);
            _targetFollower.Init(player.transform);
            _currentPlayerHealth = player.GetComponent<PlayerHealth>();
            _currentPlayerWallet = player.Wallet;
            _playerUI.SignToPlayersValuesChanges(_currentPlayerHealth, _currentPlayerWallet);

           

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

            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            agent.Warp(transform.position);
        }
    }
}
