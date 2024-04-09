using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class Barracks : Building
    {
        [SerializeField] private UnitsFactory _unitsFactory;
        private ColliderPanelEventer _eventer;

        private void OnEnable()
        {
            _eventer = GetComponentInChildren<ColliderPanelEventer>();
            _eventer.SpawnObjectButtonClicked += SpawnUnit;
        }

        private void OnDisable()
        {
            _eventer.SpawnObjectButtonClicked -= SpawnUnit;
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