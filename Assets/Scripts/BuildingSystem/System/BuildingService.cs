using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem
{
    internal class BuildingService : MonoBehaviour
    {
        [SerializeField] private BuildingUI _buildingUI;
        [SerializeField] private List<BuildPoint> _buildPoints;  
        [SerializeField] private List<Building> _prefabsOfBuildings;

        private Transform _currentPlayersTransform;
        private PlayerWallet _currentPlayersWallet;
        private int _currentCostToBuild;
        private bool _canBuild;
        private Building _currentBuilding;

        private void OnEnable()
        {
            SignToBuildingsPointEvents();
            _buildingUI.BuildButtonClicked += Build;
        }

        private void OnDisable()
        {
            UnSignToBuildingsPointEvents();
            _buildingUI.BuildButtonClicked -= Build;
        }
       
        private void Build(PlayerWallet wallet)   
        {
           
 
            for (int i = 0; i < _buildPoints.Count; i++)
            {
                if (_buildPoints[i].SpotToPlaceBuilding != null && _buildPoints[i].IsOccupied == false && _currentPlayersTransform == _buildPoints[i].transform)
                {
                    for (int j = 0; j < _prefabsOfBuildings.Count; j++)
                    {
                        if (_buildPoints[i].BuildingPointIndex == _prefabsOfBuildings[j].IndexOfBuilding && _buildPoints[i].CostToBuild <= wallet.Coins)
                        {
                            _currentBuilding = Instantiate(_prefabsOfBuildings[j], _buildPoints[i].SpotToPlaceBuilding);
                            _buildPoints[i].SignToCurrentBuilding(_currentBuilding);
                             _canBuild = false;
                             _buildPoints[i].TakeSpot();
                             _buildPoints[i].TryToDeActiveIconOfBuildPoint();
                             _buildingUI.ToggleBuildButton(BuildingUiHash.BuildButtonIndex, wallet, _currentCostToBuild, _canBuild);
                             wallet.SpendCoins(_buildPoints[i].CostToBuild);
                        }
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

        private void OnPlayerWentIn(Transform spotOfPlayer, PlayerWallet wallet)  // ��� �������� ������ �� ������
        {
            _currentPlayersTransform = spotOfPlayer;
            
            _canBuild = true;

            for (int i = 0; i < _buildPoints.Count; i++)
            {
                if (_buildPoints[i].transform == spotOfPlayer)
                {
                    _currentCostToBuild = _buildPoints[i].CostToBuild;

                    _currentPlayersWallet = wallet;
                    _buildingUI.ToggleBuildButton(BuildingUiHash.BuildButtonIndex, _currentPlayersWallet, _currentCostToBuild, _canBuild);    
                }
            }
        }

        private void OnPlayerWentOut(PlayerWallet wallet) 
        {
            _canBuild = false;
            _currentPlayersWallet = wallet;
            _buildingUI.ToggleBuildButton(BuildingUiHash.BuildButtonIndex, _currentPlayersWallet, _currentCostToBuild, _canBuild);
        }
    }
}

   

   