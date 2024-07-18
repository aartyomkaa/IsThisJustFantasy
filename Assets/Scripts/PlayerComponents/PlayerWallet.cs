using System;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerWallet 
    {
        //Сделать инстантс в классах, которые используют


        private int _coins = 100;

        public event Action<int> CoinsChanged;

        public int Coins => _coins;

        public void SpendCoins(int amount)
        {
            if (_coins >= amount)
            {
                _coins -= amount;
                CoinsChanged?.Invoke(_coins);
            }
        }

        public void AddCoins(int amount)
        {
            _coins += amount;
            CoinsChanged?.Invoke(_coins);
        }   
    }
}