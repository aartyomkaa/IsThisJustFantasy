using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class MainBuilding : Building
    {
        private PlayerWallet _playerWallet;

        private int _valueToHeal = 50;

        private void OnEnable()
        {
            Eventer.FirstButtonClicked += HealPlayer;
        }

        private void Start()
        {
            _playerWallet = new PlayerWallet();
        }

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= HealPlayer;
        }

        private void HealPlayer(Player player, int costToBuy, int buttonIndex)
        {
            if(player.gameObject.GetComponent<PlayerHealth>() != null)
            {
                PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth>();

                if (_playerWallet.Coins >= costToBuy && playerHealth.Health < playerHealth.MaxHealth)
                {
                    player.GetComponent<PlayerHealth>().Heal(_valueToHeal);
                    _playerWallet.SpendCoins(costToBuy);
                }
            }     
        }
    }
}