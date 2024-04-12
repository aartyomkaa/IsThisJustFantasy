using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerComponents
{
    internal class Player : MonoBehaviour
    {
        private PlayerWallet _wallet;

        public PlayerWallet Wallet => _wallet;

        private void Awake()
        {
            _wallet = new PlayerWallet();
        }
    }
}