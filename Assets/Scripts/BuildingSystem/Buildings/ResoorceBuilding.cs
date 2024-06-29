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

        private int _currentIndexOfChestSpawnPoint;
        private int _firstChestSpawnPoint = 0;
        private List<ChestSpawnPoint> _currentSpawnPoints;

        private void OnEnable()
        {
            Eventer.FirstButtonClicked += SpawnChest;
            Eventer.SecondButtonClicked += SpawnChest;
        }

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= SpawnChest;
            Eventer.SecondButtonClicked -= SpawnChest;
        }

        public void SetChestsSpawnPoints(ChestSpawnerPointsContainer chestSpawnPoints)
        {
            _currentSpawnPoints = chestSpawnPoints.SpawnPoints;
        }

        private void SpawnChest(Player player, int costToBuy, int buttonIndex)   
        {
            if (_currentSpawnPoints.Count != 0)
            {
                if (buttonIndex == UiHash.CoinsButtonIndex)
                {
                    if (player.Wallet.Coins >= costToBuy)
                    {
                        int _lastChestSpawnPoint = _currentSpawnPoints.Count;
                        _currentIndexOfChestSpawnPoint = Random.Range(_firstChestSpawnPoint, _lastChestSpawnPoint);
                        Chest chestToSpawn = Instantiate(_prefabOfChest, _currentSpawnPoints[_currentIndexOfChestSpawnPoint].transform);
                        chestToSpawn.SetCountOfCoins(_currentSpawnPoints[_currentIndexOfChestSpawnPoint].CoinsOfChest);
                        _currentSpawnPoints.RemoveAt(_currentIndexOfChestSpawnPoint);
                        player.Wallet.SpendCoins(costToBuy);
                    }
                }

                if (buttonIndex == UiHash.AdButtonIndex)
                {
                    int _lastChestSpawnPoint = _currentSpawnPoints.Count;
                    _currentIndexOfChestSpawnPoint = Random.Range(_firstChestSpawnPoint, _lastChestSpawnPoint);
                    Chest chestToSpawn = Instantiate(_prefabOfChest, _currentSpawnPoints[_currentIndexOfChestSpawnPoint].transform);
                    chestToSpawn.SetCountOfCoins(_currentSpawnPoints[_currentIndexOfChestSpawnPoint].CoinsOfChest);
                    _currentSpawnPoints.RemoveAt(_currentIndexOfChestSpawnPoint);
                }
            }  
        }
    }
}