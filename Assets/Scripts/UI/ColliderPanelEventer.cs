using Assets.Scripts.PlayerComponents;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class ColliderPanelEventer : MonoBehaviour
    {
        [SerializeField] private int _costToBuy;
        [SerializeField] GameObject _panelToShow;
        [SerializeField] private Button _spawnObjectButton;
        [SerializeField] private Button _spawnObjectForAdButton;
        [SerializeField] private Button _extraButton;   // кнопка начала новой волны, пока не знаю как назвать

        private PlayerWallet _currentPlayersWallet;

        public Action<PlayerWallet, int> SpawnObjectButtonClicked;
        public Action SpawnObjectForAdButtonClicked;
        public Action ExtraButtonButtonClicked;

        private void Start()
        {
            SetCostToButtonText(_spawnObjectButton, _costToBuy);
        }
       
        private void OnEnable()
        {
            _spawnObjectButton.onClick.AddListener(OnSpawnObjectButtonClicked);
            _spawnObjectForAdButton.onClick.AddListener(OnSpawnObjectForAdButtonClicked);
           
            if(_extraButton != null)
            {
                _extraButton.onClick.AddListener(OnExtraButtonClicked);
            }  
        }
        private void OnDisable()
        {
            _spawnObjectButton.onClick.RemoveListener(OnSpawnObjectButtonClicked);
            _spawnObjectForAdButton.onClick.RemoveListener(OnSpawnObjectForAdButtonClicked);

            if (_extraButton != null)
            {
                _extraButton.onClick.RemoveListener(OnExtraButtonClicked);
            }        
        }
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _panelToShow.gameObject.SetActive(true);
                _currentPlayersWallet = player.Wallet;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _panelToShow.gameObject.SetActive(false);
            }
        }

        private void SetCostToButtonText(Button activeButton, int costToBuy)
        {
            activeButton.GetComponentInChildren<TMP_Text>().text += costToBuy;
        }

        public void OnSpawnObjectButtonClicked()
        {
            SpawnObjectButtonClicked?.Invoke(_currentPlayersWallet, _costToBuy);
        }

        public void OnSpawnObjectForAdButtonClicked()
        {
            SpawnObjectForAdButtonClicked?.Invoke();
        }

        public void OnExtraButtonClicked()
        {
            ExtraButtonButtonClicked?.Invoke();
        }
    }
}