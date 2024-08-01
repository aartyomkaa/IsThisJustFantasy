using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class Barracks : Building
    {
        private UnitsFactory _unitsFactory;
        private PlayerWallet _playerWallet;

        private void OnEnable()
        {
           if(GetComponentInChildren<UnitsFactory>() != null)
            {
                _unitsFactory = GetComponentInChildren<UnitsFactory>();
            }
           
            Eventer.FirstButtonClicked += SpawnUnit;
            Eventer.SecondButtonClicked += SpawnUnit;
        }

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= SpawnUnit;
            Eventer.SecondButtonClicked -= SpawnUnit;
        }

        public void Init(SelectedUnitsHandler handler)
        {
            _playerWallet = new PlayerWallet();
            _unitsFactory.Init(handler);
        }

        private void SpawnUnit(Player player, int costToBuy, int buttonIndex)  
        {  
            if(buttonIndex == UiHash.CoinsButtonIndex)
            {
                if (_playerWallet.Coins >= costToBuy)
                {
                    _unitsFactory.Spawn();
                    _playerWallet.SpendCoins(costToBuy);
                }
            }
           
            if(buttonIndex == UiHash.AdButtonIndex)
            {
                _unitsFactory.Spawn();
            }   
        }
    }
}