using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class Barracks : Building
    {
        [SerializeField] private UnitsFactory _unitsFactory;
        [SerializeField] private Button _barracksButton;

        private void OnEnable()
        {
            BuildingUI.SpawnUnitButtonClicked += SpawnUnit;
            _barracksButton.onClick.AddListener(SpawnUnitTest);
        }

        private void OnDisable()
        {
            BuildingUI.SpawnUnitButtonClicked -= SpawnUnit;
            _barracksButton.onClick.RemoveListener(SpawnUnitTest);
        }

        private void SpawnUnit(PlayerWallet wallet, int costToBuy)  
        {
            if(wallet.Coins >= costToBuy)
            {
                _unitsFactory.Spawn();
                wallet.SpendCoins(costToBuy);
            }  
        }

        private void SpawnUnitTest()
        {
            _unitsFactory.Spawn();
        }
    }
}