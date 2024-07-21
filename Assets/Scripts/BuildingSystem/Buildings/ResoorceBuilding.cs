using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.Props.Chest;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class ResoorceBuilding : Building
    {  
        [SerializeField] private Chest _prefabOfChest;

        private PlayerWallet _playerWallet;
        private int _currentIndexOfChestSpawnPoint;
        private int _firstChestSpawnPoint = 0;
        private List<ChestSpawnPoint> _currentSpawnPoints;

        private void OnEnable()
        {
            Eventer.FirstButtonClicked += OnPrimaryButtonClicked;
            Eventer.SecondButtonClicked += OnPrimaryButtonClicked;
        }

        private void Start()
        {
            _playerWallet = new PlayerWallet();
        }

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= OnPrimaryButtonClicked;
            Eventer.SecondButtonClicked -= OnPrimaryButtonClicked;
        }

        public void SetChestsSpawnPoints(ChestSpawnerPointsContainer chestSpawnPoints)
        {
            _currentSpawnPoints = chestSpawnPoints.SpawnPoints;
        }
        
        private void OnPrimaryButtonClicked(Player player, int costToBuy, int buttonIndex)   
        {
            if (_currentSpawnPoints.Count != 0)
            {
                if (buttonIndex == UiHash.CoinsButtonIndex)
                {
                    if (_playerWallet.Coins >= costToBuy)
                    {
                        Spawn();
                        _playerWallet.SpendCoins(costToBuy);
                    }
                }

                if (buttonIndex == UiHash.AdButtonIndex)
                {
                    Spawn();
                    _currentSpawnPoints.RemoveAt(_currentIndexOfChestSpawnPoint);
                }
            }  
        }

        private void Spawn()
        {
            int _lastChestSpawnPoint = _currentSpawnPoints.Count;
            _currentIndexOfChestSpawnPoint = Random.Range(_firstChestSpawnPoint, _lastChestSpawnPoint);
            Chest chestToSpawn = Instantiate(_prefabOfChest, _currentSpawnPoints[_currentIndexOfChestSpawnPoint].transform);
            chestToSpawn.SetCountOfCoins(_currentSpawnPoints[_currentIndexOfChestSpawnPoint].CoinsOfChest);
            _currentSpawnPoints.RemoveAt(_currentIndexOfChestSpawnPoint);
        }
    }
}