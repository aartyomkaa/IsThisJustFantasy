using Assets.Scripts.Constants;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class EnemyBuilding : MonoBehaviour
    {
        [SerializeField] private ColliderPanelEventer _eventer;

        private EnemyFactory _enemyFactory;

        private bool _isIncrease;

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
