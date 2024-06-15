using Assets.Scripts.PlayerComponents;
using UnityEngine;


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
            if(player.Wallet.Coins >= costToBuy)
            {
                player.GetComponent<PlayerHealth>().Heal(_valueToHeal);
                player.Wallet.SpendCoins(costToBuy);
            }    
        }
    }
}