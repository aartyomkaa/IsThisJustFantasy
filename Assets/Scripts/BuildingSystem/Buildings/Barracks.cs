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
            _unitsFactory = GetComponentInChildren<UnitsFactory>();
            Eventer.FirstButtonClicked += SpawnUnit;
            Eventer.SecondButtonClicked += SpawnUnit;
        }

        private void Start()
        {
            _playerWallet = new PlayerWallet();
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