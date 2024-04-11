using Assets.Scripts.PlayerComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class PlayerUI : MonoBehaviour
    {

        private PlayerHealth _currentplayerHealth;
        private PlayerWallet _currentplayerWallet;

        public void SignToPlayersValuesChanges(PlayerHealth health, PlayerWallet coins)
        {
            _currentplayerHealth = health;
            _currentplayerWallet = coins;
            _currentplayerHealth.ValueChanged += OnHealthChanged;
            //_currentplayerWallet.CoinsChanged += OnCoinsChanged;
        }

        private void OnDisable()
        {
            _currentplayerHealth.ValueChanged -= OnHealthChanged;
            _currentplayerWallet.CoinsChanged -= OnCoinsChanged;
        }

        private void OnHealthChanged(float health)
        {
            Debug.Log("Здоровье игрока - " + health);
        }

        private void OnCoinsChanged(int coins)
        {
            Debug.Log("Монеты игрока - " + coins);
        }
    }
}