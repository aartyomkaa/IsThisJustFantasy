using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.Props.Chest;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuildingService : MonoBehaviour
    {
        [SerializeField] private BuilderButton _builder;
        [SerializeField] private List<BuildPoint> _buildPoints;  
        [SerializeField] private Tower _tower;
        [SerializeField] private Barracks _barracks;
        [SerializeField] private ResoorceBuilding _resoorceBuilding;
        [SerializeField] private ChestSpawnerPointsContainer _chestSpawnPoints;

        private BuildingSpawner _buildingSpawner;
        private Transform _currentPlayersTransform;
        private PlayerWallet _currentPlayersWallet;
        private int _currentCostToBuild;
        private bool _canBuild;

        private void Start()
        {
            _buildingSpawner = new BuildingSpawner(_tower, _barracks, _resoorceBuilding);
        }

        private void OnEnable()
        {
            SignToBuildingsPointEvents();
            _builder.BuildButtonClicked += Build;
        }

        private void OnDisable()
        {
            UnSignToBuildingsPointEvents();
            _builder.BuildButtonClicked -= Build;
        }
       
        private void Build(PlayerWallet wallet)   
        {  
            for (int i = 0; i < _buildPoints.Count; i++)
            {
                if (_buildPoints[i].SpotToPlaceBuilding != null && _buildPoints[i].IsOccupied == false && _currentPlayersTransform == _buildPoints[i].transform)
                {
                        if(_buildPoints[i].CostToBuild <= wallet.Coins)
                        {
                            _buildingSpawner.Spawn(_buildPoints[i].BuildingPointIndex, _buildPoints[i].SpotToPlaceBuilding, _chestSpawnPoints);
                            _buildPoints[i].TakeSpot();
                            _buildPoints[i].SignToCurrentBuilding(_buildingSpawner.CurrentBuilding);
                            _canBuild = false;
                            _buildPoints[i].TryToDeActiveIconOfBuildPoint();
                            _builder.ToggleButton(wallet, _currentCostToBuild, _canBuild);
                            wallet.SpendCoins(_buildPoints[i].CostToBuild);
                        }
                }
            }
        }

        private void SignToBuildingsPointEvents()
        {
            for (int i = 0; i < _buildPoints.Count; i++)
            {
                _buildPoints[i].PlayerWentIn += OnPlayerWentIn;
                _buildPoints[i].PlayerWentOut += OnPlayerWentOut;
            }
        }

        private void UnSignToBuildingsPointEvents()
        {
            for (int i = 0; i < _buildPoints.Count; i++)
            {
                _buildPoints[i].PlayerWentIn -= OnPlayerWentIn;
                _buildPoints[i].PlayerWentOut -= OnPlayerWentOut;
            }
        }

        private void OnPlayerWentIn(Transform spotOfPlayer, PlayerWallet wallet)  
        {
            _currentPlayersTransform = spotOfPlayer;
            _canBuild = true;

            for (int i = 0; i < _buildPoints.Count; i++)
            {
                if (_buildPoints[i].transform == spotOfPlayer)
                {
                    _currentCostToBuild = _buildPoints[i].CostToBuild;

                    _currentPlayersWallet = wallet;
                    _builder.ToggleButton(_currentPlayersWallet, _currentCostToBuild, _canBuild);    
                }
            }
        }
        private void OnPlayerWentOut(PlayerWallet wallet) 
        {
            _canBuild = false;
            _currentPlayersWallet = wallet;
            _builder.ToggleButton(_currentPlayersWallet, _currentCostToBuild, _canBuild);
        }
    }
}

   

   