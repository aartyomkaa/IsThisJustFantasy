using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuildingService : MonoBehaviour
    {
        [SerializeField] private BuildingUI _buildingUI;
        [SerializeField] private List<BuildPoint> _buildPoints;  
        [SerializeField] private Tower _tower;
        [SerializeField] private Barracks _barracks;
        [SerializeField] private ResoorceBuilding _resoorceBuilding;

        private Transform _currentPlayersTransform;
        private PlayerWallet _currentPlayersWallet;
        private int _currentCostToBuild;
        private bool _canBuild;
        private Building _currentBuilding;
        private BuildingsPool _buildingsPool;

        private void Start()
        {
            _buildingsPool = new BuildingsPool(_tower, _barracks, _resoorceBuilding);
        }

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

                        if(_buildPoints[i].CostToBuild <= wallet.Coins)
                        {
                            _currentBuilding = _buildingsPool.GetBuilding(_buildPoints[i].BuildingPointIndex); 
                             _currentBuilding.Transform.parent = _buildPoints[i].SpotToPlaceBuilding;
                              _buildPoints[i].TakeSpot();

                             _buildPoints[i].SignToCurrentBuilding(_currentBuilding);
                            _canBuild = false;
                            
                            _buildPoints[i].TryToDeActiveIconOfBuildPoint();
                            _buildingUI.ToggleBuildButton(BuildingUiHash.BuildButtonIndex, wallet, _currentCostToBuild, _canBuild);
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

   

   