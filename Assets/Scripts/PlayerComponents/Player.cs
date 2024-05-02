using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    internal class Player : MonoBehaviour
    {
        [SerializeField] private List<PlayerComponent> _components;
        [SerializeField] private PlayerData[] _levels;

        private int _currentLevel;
        private int _maxLevel = 4;
        private PlayerWallet _wallet;

        public int CurrentLevel => _currentLevel;
        public PlayerWallet Wallet => _wallet;

        private void Awake()
        {
            _wallet = new PlayerWallet();
            
            _currentLevel = 0;

            for (int i =  0; i < _levels.Length; i++)
            {
                if (i == _currentLevel)
                {
                    foreach (PlayerComponent component in _components)
                    {
                        component.Init(_levels[i]);
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
            }
        }
    }
}