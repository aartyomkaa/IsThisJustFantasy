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

        private PlayerHealth _currentplayerHealth;
        private PlayerWallet _currentplayerWallet;

        public void SignToPlayersValuesChanges(PlayerHealth health, PlayerWallet coins)
        {
            _currentplayerHealth = health;
            _health.value = _currentplayerHealth.Value;

            _currentplayerWallet = coins;
            _coins.text = _currentplayerWallet.Coins.ToString();

            _currentplayerHealth.ValueChanged += OnHealthChanged;
            _currentplayerWallet.CoinsChanged += OnCoinsChanged;
        }

        private void OnDisable()
        {
            _currentplayerHealth.ValueChanged -= OnHealthChanged;
            _currentplayerWallet.CoinsChanged -= OnCoinsChanged;
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