using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class Barracks : Building
    {
        private UnitsFactory _unitsFactory;

        private void OnEnable()
        {
            _unitsFactory = GetComponentInChildren<UnitsFactory>();
            Eventer.FirstButtonClicked += SpawnUnit;
            Eventer.SecondButtonClicked += SpawnUnit;
        }

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= SpawnUnit;
            Eventer.SecondButtonClicked -= SpawnUnit;
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