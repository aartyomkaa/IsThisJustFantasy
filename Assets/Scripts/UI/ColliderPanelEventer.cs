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
        [SerializeField] private Button _spawnObjectButton;    //+ один выброс за игровые деньги   
        [SerializeField] private Button _spawnObjectForAdButton;   // - минус один выброс за реламу
        [SerializeField] private Button _extraButton;   // кнопка начала новой волны, пока не знаю как назвать
        [SerializeField] private int _panelMoveXValue;

        private PlayerWallet _currentPlayersWallet;
        private bool _currentStatus;
        private float _changeScaleSpeed = 0.15f;
       

        public Action<PlayerWallet, int> SpawnObjectButtonClicked;
        public Action SpawnObjectForAdButtonClicked;
        public Action ExtraButtonClicked;

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
                _currentStatus = true;
                Open();
                _currentPlayersWallet = player.Wallet;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _currentStatus = false;
                Close();
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
            ExtraButtonClicked?.Invoke();
        }

        private void Open()
        {
            ChangeActiveStatus();
            _panelToShow.GetComponent<RectTransform>().LeanSetLocalPosX(transform.position.x + _panelMoveXValue);
            LeanTween.moveX(_panelToShow.GetComponent<RectTransform>(), - _panelMoveXValue, _changeScaleSpeed);
           
        }

        private void Close()
        {
            LeanTween.moveX(_panelToShow.GetComponent<RectTransform>(), _panelMoveXValue, _changeScaleSpeed).setOnComplete(ChangeActiveStatus);
        }

        private void ChangeActiveStatus()
        {
            _panelToShow.gameObject.SetActive(_currentStatus);
        }
    }
}