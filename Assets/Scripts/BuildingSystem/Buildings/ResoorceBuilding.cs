using Assets.Scripts.PlayerComponents;
using Assets.Scripts.Props.Chest;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class ResoorceBuilding : Building
    {  
        [SerializeField] private Chest _prefabOfChest;

        private int _currentIndexOfChestSpawnPoint;
        private ColliderPanelEventer _eventer;
        private int _firstChestSpawnPoint = 0;
        private List<ChestSpawnPoint> _currentSpawnPoints;

        public void SetChestsSpawnPoints(ChestSpawnerPointsContainer chestSpawnPoints)
        {
            _currentSpawnPoints = chestSpawnPoints.SpawnPoints;     
        }

        private void OnEnable()
        {
            _eventer = GetComponentInChildren<ColliderPanelEventer>();
            _eventer.SpawnObjectButtonClicked += SpawnChest;
        }

        private void OnDisable()
        {
             _eventer.SpawnObjectButtonClicked -= SpawnChest;
        }

        private void SpawnChest(PlayerWallet wallet, int costToBuy)   
        {
            if(_currentSpawnPoints.Count != 0)
            {  
                if(wallet.Coins >= costToBuy)
                {
                    int _lastChestSpawnPoint = _currentSpawnPoints.Count;
                    _currentIndexOfChestSpawnPoint = Random.Range(_firstChestSpawnPoint, _lastChestSpawnPoint);
                    Chest chestToSpawn = Instantiate(_prefabOfChest, _currentSpawnPoints[_currentIndexOfChestSpawnPoint].transform);
                     chestToSpawn.SetCountOfCoins(_currentSpawnPoints[_currentIndexOfChestSpawnPoint].CoinsOfChest);
                    _currentSpawnPoints.RemoveAt(_currentIndexOfChestSpawnPoint);
                    wallet.SpendCoins(costToBuy);
                }   
            }  
        }
    }
}