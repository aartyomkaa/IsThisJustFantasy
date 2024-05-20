using Assets.Scripts.PlayerComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider _health;
        [SerializeField] private TMP_Text _coins;
        [SerializeField] private TMP_Text _level;

        private PlayerHealth _currentPlayerHealth;
        private PlayerWallet _currentPlayerWallet;
        private int _currentPlayerLevel;

        public void SignToPlayersValuesChanges(PlayerHealth health, PlayerWallet coins, int playerLevel)
        {
            _currentPlayerHealth = health;
            _health.value = _currentPlayerHealth.Health;

            OnLevelChanged(playerLevel);

            _currentPlayerWallet = coins;
            _coins.text = _currentPlayerWallet.Coins.ToString();

            _currentPlayerHealth.ValueChanged += OnHealthChanged;
            _currentPlayerWallet.CoinsChanged += OnCoinsChanged;
        }

        public void OnLevelChanged(int newPlayerLevel)
        {
            _currentPlayerLevel = newPlayerLevel;
            _level.text = _currentPlayerLevel.ToString();
        }

        private void OnDisable()
        {
            _currentPlayerHealth.ValueChanged -= OnHealthChanged;
            _currentPlayerWallet.CoinsChanged -= OnCoinsChanged;
        }

        private void OnHealthChanged(float health)
        {
            _health.value = health;
        }

        private void OnCoinsChanged(int coins)
        {
            _coins.text = coins.ToString();
        }
    }
}