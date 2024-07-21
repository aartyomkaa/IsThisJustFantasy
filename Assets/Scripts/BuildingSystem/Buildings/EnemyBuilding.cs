using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Constants;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class EnemyBuilding : MonoBehaviour
    {
        [SerializeField] private ColliderPanelEventer _eventer;

        private EnemyFactory _enemyFactory;
        private bool _isIncrease;
        
        public event Action<ColliderPanelEventer> BuildWithEventorWasMade;

        public Button AdButton => _eventer.AdButton;
       
        public ColliderPanelEventer EventerToSend => _eventer;

        private void Awake()
        {
            AnnounceOfCreation();
        }
       
        private void OnEnable()
        {
            _enemyFactory = GetComponentInChildren<EnemyFactory>();
            _enemyFactory.WaveStarted += OnWaveStart;
            _enemyFactory.WaveEnded += OnWaveEnd;
            _eventer.FirstButtonClicked += OnPrimaryButtonClicked;
            _eventer.SecondButtonClicked += OnPrimaryButtonClicked;
            _eventer.ExtraButtonClicked += OnExtraButtonClicked;
        }

        private void OnDisable()
        {
            _enemyFactory.WaveStarted -= OnWaveStart;
            _enemyFactory.WaveEnded -= OnWaveEnd;
            _eventer.FirstButtonClicked -= OnPrimaryButtonClicked;
            _eventer.SecondButtonClicked -= OnPrimaryButtonClicked;
            _eventer.ExtraButtonClicked -= OnExtraButtonClicked;
        }

        public void AnnounceOfCreation()
        {
            if (_eventer != null)
            {
                BuildWithEventorWasMade?.Invoke(_eventer);
            }
        }

        private void OnPrimaryButtonClicked(Player player, int costToBuy, int buttonIndex)
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

        private void OnExtraButtonClicked()
        {
            _enemyFactory.StartWave();
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
