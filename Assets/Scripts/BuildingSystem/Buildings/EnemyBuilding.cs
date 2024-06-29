using UnityEngine;
using Assets.Scripts.Constants;
using Assets.Scripts.EnemyComponents;
using UnityEngine.UI;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI;
using System;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class EnemyBuilding : MonoBehaviour
    {
        [SerializeField] private ColliderPanelEventer _eventer;

        private EnemyFactory _enemyFactory;
        private bool _isIncrease;

        public Button AdButton => _eventer.SecondButton;
       
        public ColliderPanelEventer EventerToSend => _eventer;
        public event Action<ColliderPanelEventer> BuildWithEventorWasMade;

        private void Awake()
        {
            AnnounceOfCreation();
        }
       
        private void OnEnable()
        {
            _enemyFactory = GetComponentInChildren<EnemyFactory>();
            _enemyFactory.WaveStarted += OnWaveStart;
            _enemyFactory.WaveEnded += OnWaveEnd;
            _eventer.FirstButtonClicked += ChangeSpawnAmount;
            _eventer.SecondButtonClicked += ChangeSpawnAmount;
            _eventer.ExtraButtonClicked += _enemyFactory.StartWave;
        }

        private void OnDisable()
        {
            _enemyFactory.WaveStarted -= OnWaveStart;
            _enemyFactory.WaveEnded -= OnWaveEnd;
            _eventer.FirstButtonClicked -= ChangeSpawnAmount;
            _eventer.SecondButtonClicked -= ChangeSpawnAmount;
            _eventer.ExtraButtonClicked -= _enemyFactory.StartWave;
        }

        public void AnnounceOfCreation()
        {
            if (_eventer != null)
            {
                BuildWithEventorWasMade?.Invoke(_eventer);
            }
        }

        private void ChangeSpawnAmount(Player player, int costToBuy, int buttonIndex)
        {
            if (buttonIndex == UiHash.CoinsButtonIndex && player.Wallet.Coins >= costToBuy)
            {
                _isIncrease = true;
                player.Wallet.SpendCoins(costToBuy);
            }

            if (buttonIndex == UiHash.AdButtonIndex)
            {
                _isIncrease = false;
            }

            _enemyFactory.ChangeSpawnAmount(_isIncrease);
        }

        private void OnWaveStart(int spawnAmount)
        {
            _eventer.gameObject.SetActive(false);
        }

        private void OnWaveEnd()
        {
            _eventer.gameObject.SetActive(true);
        }
    }
}
