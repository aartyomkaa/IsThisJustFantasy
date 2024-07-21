using System;
using Assets.Scripts.Constants;
using Agava.YandexGames.Utility;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerWallet 
    {
        private int _coins = 100;
        private int _newLevelCoinsAmount = 100;
        
        public event Action<int> CoinsChanged;

        public int Coins => _coins;

        public void SpendCoins(int amount)
        {
            if (_coins >= amount)
            {
                _coins -= amount;
                SaveCoins();
                CoinsChanged?.Invoke(_coins);
            }
        }

        public void AddCoins(int amount)
        {
            _coins += amount;
            SaveCoins();
            CoinsChanged?.Invoke(_coins);
        }

        public void Reset()
        {
            _coins = _newLevelCoinsAmount;
        }

        private void SaveCoins()
        {
            PlayerPrefs.SetInt(PlayerConfigs.Coins, _coins);
        }
    }
}