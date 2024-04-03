using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class Barracks : Building
    {
        [SerializeField] private UnitsFactory _unitsFactory;

        private void OnEnable()
        {
            BuildingUI.SpawnUnitButtonClicked += SpawnUnit;
        }

        private void OnDisable()
        {
            BuildingUI.SpawnUnitButtonClicked -= SpawnUnit;
        }

        private void SpawnUnit(PlayerWallet wallet, int costToBuy)  
        {
            if(wallet.Coins >= costToBuy)
            {
                _unitsFactory.Spawn();
                wallet.SpendCoins(costToBuy);
            }  
        }
    }
}