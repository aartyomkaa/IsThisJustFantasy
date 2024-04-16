using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class Barracks : Building
    {
        private UnitsFactory _unitsFactory;
        private ColliderPanelEventer _eventer;

        private void OnEnable()
        {
            _eventer = GetComponentInChildren<ColliderPanelEventer>();
            _unitsFactory = GetComponentInChildren<UnitsFactory>();   
            _eventer.FirstButtonClicked += SpawnUnit;
            _eventer.SecondButtonClicked += SpawnUnit;
        }

        private void OnDisable()
        {
            _eventer.FirstButtonClicked -= SpawnUnit;
            _eventer.SecondButtonClicked -= SpawnUnit;
        }

        private void SpawnUnit(Player player, int costToBuy, int buttonIndex)  
        {  
            if(buttonIndex == UiHash.CoinsButtonIndex)
            {
                if (player.Wallet.Coins >= costToBuy)
                {
                    _unitsFactory.Spawn();
                    player.Wallet.SpendCoins(costToBuy);
                }
            }
           
            if(buttonIndex == UiHash.AdButtonIndex)
            {
                _unitsFactory.Spawn();
            }   
        }
    }
}