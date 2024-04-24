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

        private PlayerWallet _wallet;

        public int CurrentLevel;

        public PlayerWallet Wallet => _wallet;

        private void Awake()
        {
            _wallet = new PlayerWallet();
            
            _currentLevel = PlayerPrefs.GetInt(PlayerConfigs.PlayerLevel);

            for (int i =  0; i < _levels.Length; i++)
            {
                if (i == _currentLevel)
                {
                    foreach (PlayerComponent component in _components)
                    {
                        component.Init(_levels[i]);
                    }
                }
            }
        }

        public void LevelUp()
        {
            _currentLevel++;
            PlayerPrefs.SetInt(PlayerConfigs.PlayerLevel, _currentLevel);
        }
    }
}