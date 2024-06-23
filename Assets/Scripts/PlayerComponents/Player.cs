using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

namespace Assets.Scripts.PlayerComponents
{
    internal class Player : MonoBehaviour
    {
        [SerializeField] private List<PlayerComponent> _components;
        [SerializeField] private PlayerData[] _levels;

        private int _currentLevel;
        private int _maxLevel = 6;
        private PlayerWallet _wallet;

        public int CurrentLevel => _currentLevel;
        public PlayerWallet Wallet => _wallet;

        public event Action<int> LevelChanged;

        private void Awake()
        {
            _wallet = new PlayerWallet();
            PlayerSFX sfx = GetComponentInChildren<PlayerSFX>();

            _currentLevel = PlayerPrefs.GetInt(PlayerConfigs.PlayerLevel);

#if !UNITY_WEBGL && UNITY_EDITOR            
            _currentLevel = 0;
#endif

            for (int i =  0; i < _levels.Length; i++)
            {
                if (i == _currentLevel)
                {
                    foreach (PlayerComponent component in _components)
                    {
                        component.Init(_levels[i], sfx);
                    }

                    break;
                }
            }
        }

        public void LevelUp()
        {
            if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
                PlayerPrefs.SetInt(PlayerConfigs.PlayerLevel, _currentLevel);
                LevelChanged?.Invoke(_currentLevel);
            }
        }
    }
}