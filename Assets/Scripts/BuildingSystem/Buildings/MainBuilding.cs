using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class MainBuilding : Building
    {
        //[SerializeField] private /// обучалка

        private int _valueToHeal = 50;
       
        
        private ColliderPanelEventer _eventer;
       // private PlayerHealth _currentPlayerHealth;

        private void OnEnable()
        {
            _eventer = GetComponentInChildren<ColliderPanelEventer>();
            _eventer.FirstButtonClicked += HealPlayer;

        }

        private void OnDisable()
        {
            _eventer.FirstButtonClicked -= HealPlayer;
        }

        private void HealPlayer(Player player, int costToBuy, int buttonIndex)
        {
            if(player.Wallet.Coins >= costToBuy)
            {
                player.GetComponent<PlayerHealth>().TakeHeal(_valueToHeal);
            }    
        }
    }
}