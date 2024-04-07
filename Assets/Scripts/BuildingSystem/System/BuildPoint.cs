using UnityEngine;
using Assets.Scripts.PlayerComponents;
using System;


namespace Assets.Scripts.BuildingSystem
{
    internal class BuildPoint : MonoBehaviour
    {
        [SerializeField] private Transform _spotToPlaceBuilding;
        [SerializeField] private int _buildingPointIndex;
        [SerializeField] private GameObject _iconOfBuildPoint;
        [SerializeField] private int _costToBuild;

        private bool _isOccupied;
        private int speedOfRotateVisualObject = 200;
        private Building _currentBuilding;
        private int _numberToSetRaiseValue = 5;

        public Transform SpotToPlaceBuilding => _spotToPlaceBuilding;
        public int BuildingPointIndex => _buildingPointIndex;
        public bool IsOccupied => _isOccupied;
        public int CostToBuild => _costToBuild;

        public Action<Transform,PlayerWallet> PlayerWentIn;
        public Action<PlayerWallet> PlayerWentOut; 


        private void Update()
        {
            RotateIconOfBuildingPoint();
        }

        private void RotateIconOfBuildingPoint()
        {
            _iconOfBuildPoint.transform.Rotate(0, speedOfRotateVisualObject * Time.deltaTime, 0);
        }

        private void RaiseCostForNextBuilding()
        {

            int valueToRaise = _costToBuild / _numberToSetRaiseValue;
            _costToBuild += valueToRaise;
        }

        private void OnDisable()
        {
           if(_currentBuilding != null)
            {
                _currentBuilding.Destroyed -= FreeSpotToBuild;
            }  
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null && other.gameObject.TryGetComponent(out Player player))
            {
                if (_isOccupied == false) 
                {
                    PlayerWentIn?.Invoke(transform, player.Wallet);   
                }
            }
        }

       

        private void OnTriggerExit(Collider other)
        {
            if (other != null && other.gameObject.TryGetComponent(out Player player))
            {
                PlayerWentOut?.Invoke(player.Wallet);     
            }
        }

        public void TakeSpot()
        {
            _isOccupied = true;
        }

        public void SignToCurrentBuilding(Building biulding)
        {
            _currentBuilding = biulding;

            if (_isOccupied == true && _currentBuilding.Transform.parent.position == _spotToPlaceBuilding.position) 
            {
                

                _currentBuilding.Destroyed += FreeSpotToBuild;
            }
                
        }

        private void FreeSpotToBuild(Transform buidingTransform)
        {

            if (_isOccupied == true && buidingTransform.position == _spotToPlaceBuilding.position) 
            {     
                _isOccupied = false;
                ActiveIconOfBuildPoint();
                RaiseCostForNextBuilding();
            }
        }
       
        public void ActiveIconOfBuildPoint()
        {
            _iconOfBuildPoint.SetActive(true);
        }

        public void TryToDeActiveIconOfBuildPoint()
        {
            if (_iconOfBuildPoint.gameObject.activeSelf == true)
            {
                _iconOfBuildPoint.SetActive(false);
            }
        }
    }
}