using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerWallet 
    {
        private int _coins = 1000;

        public int Coins => _coins;

        public PlayerWallet() 
        { 
            //_coins = 1000;
        }

        public void SpendCoins(int amount)
        {
            if (_coins >= amount)
            {
                _coins -= amount;
            }
        }

        public void AddCoins(int amount)
        {
            _coins += amount;
        }   
    }
}
