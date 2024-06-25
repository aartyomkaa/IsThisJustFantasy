using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.UI
{
    internal class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _coins;
        [SerializeField] private TMP_Text _level;

        private PlayerHealth _health;
        private PlayerWallet _wallet;
        private int _playerLevel;

        public void SignToPlayerValuesChanges(Player player)
        {
            _health = player.GetComponent<PlayerHealth>();
            _slider.maxValue = _health.Health;
            _slider.value = _health.Health;

            OnLevelChanged(player.CurrentLevel);

            _wallet = player.Wallet;
            _coins.text = _wallet.Coins.ToString();

            _health.ValueChanged += OnHealthChanged;
            _wallet.CoinsChanged += OnCoinsChanged;
        }

        public void OnLevelChanged(int newPlayerLevel)
        {
            _playerLevel = newPlayerLevel;
            _level.text = _playerLevel.ToString();
        }

        private void OnDisable()
        {
            _health.ValueChanged -= OnHealthChanged;
            _wallet.CoinsChanged -= OnCoinsChanged;
        }

        private void OnHealthChanged(float health)
        {
            _slider.value = health;
        }

        private void OnCoinsChanged(int coins)
        {
            _coins.text = coins.ToString();
        }
    }
}