using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class MainBuilding : Building
    {
        private int _valueToHeal = 50;

        private void OnEnable()
        {
            Eventer.FirstButtonClicked += HealPlayer;
        }

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= HealPlayer;
        }

        private void HealPlayer(Player player, int costToBuy, int buttonIndex)
        {
            PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth>();

            if (player.Wallet.Coins >= costToBuy && playerHealth.Health < playerHealth.MaxHealth)
            {
                player.GetComponent<PlayerHealth>().Heal(_valueToHeal);
                player.Wallet.SpendCoins(costToBuy);
            }    
        }
    }
}