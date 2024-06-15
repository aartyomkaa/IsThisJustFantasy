using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.YandexSDK;

namespace Assets.Scripts.UI
{
    internal class ColliderPanelEventer : MonoBehaviour
    {
        [SerializeField] private int _costToBuy;
        [SerializeField] GameObject _panelToShow;
        [SerializeField] private Button _firstButton;  
        [SerializeField] private Button _secondButton;
        [SerializeField] private Button _extraButton;
        [SerializeField] private TutorialPanel _tutorial;
        [SerializeField] private LeanToken _cost;
        [SerializeField] private InterstitialAdPopup _popupPanel;

        private Player _currentPlayer;
        private bool _isActive;
        private float _changeScaleSpeed = 0.15f;
        private int _panelMoveXValue = 228;
        private InterstitialAdTimer _timer;
        private bool _isSecondButtonOnCooldown = false;

        public Button SecondButton => _secondButton;

        public event Action<Player, int, int> FirstButtonClicked;
        public event Action<Player, int, int> SecondButtonClicked;
        public event Action ExtraButtonClicked;


       public void TakeTimer(InterstitialAdTimer timer)
        {
            _timer = timer;
            _isSecondButtonOnCooldown = _timer.IsOnCooldown;
            _timer.CooldownStarted += TurnSecondButton;
            _timer.BecomeAvailable += TurnSecondButton;
        }
        
        
        private void Start()
        {
            _cost.SetValue(_costToBuy);
            Debug.Log("я сейчас на кулдауне? " + _isSecondButtonOnCooldown);
           
        }
       
        private void OnEnable()
        {
            _firstButton.onClick.AddListener(OnFirsttButtonClicked);
            _secondButton.onClick.AddListener(OnSecondButtonClicked);
           
            if(_extraButton != null)
            {
                _extraButton.onClick.AddListener(OnExtraButtonClicked);
            }  
        }
        private void OnDisable()
        {
            _firstButton.onClick.RemoveListener(OnFirsttButtonClicked);
            _secondButton.onClick.RemoveListener(OnSecondButtonClicked);

            if (gameObject.activeSelf && _timer != null)
            {
                _timer.CooldownStarted -= TurnSecondButton;
                _timer.BecomeAvailable -= TurnSecondButton;
            }
              
            if (_extraButton != null)
            {
                _extraButton.onClick.RemoveListener(OnExtraButtonClicked);
            }

            Close();  
        }
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _isActive = true;
                Open();
                _currentPlayer = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                Close();
            }
        }

       private void TurnSecondButton(bool isOnCooldown)
        {
            _isSecondButtonOnCooldown = isOnCooldown;
        }
        
        private void Open()
        {
            ChangeActiveStatus();
            _panelToShow.GetComponent<RectTransform>().LeanSetLocalPosX(transform.position.x + _panelMoveXValue);
            LeanTween.moveX(_panelToShow.GetComponent<RectTransform>(), - _panelMoveXValue, _changeScaleSpeed);

            if (PlayerPrefs.GetInt(PlayerConfigs.HasPassedTutorial) == 0)
                _tutorial.Open();
        }

        private void Close()
        {
            _isActive = false;
            LeanTween.moveX(_panelToShow.GetComponent<RectTransform>(), _panelMoveXValue, _changeScaleSpeed).setOnComplete(ChangeActiveStatus);

            if (PlayerPrefs.GetInt(PlayerConfigs.HasPassedTutorial) == 0)
                _tutorial.Close();
        }

        private void ChangeActiveStatus()
        {
            _panelToShow.gameObject.SetActive(_isActive);
        }

        public void OnFirsttButtonClicked()
        {
            FirstButtonClicked?.Invoke(_currentPlayer, _costToBuy, UiHash.CoinsButtonIndex);
        }

        public void OnSecondButtonClicked()
        {
            if (_isSecondButtonOnCooldown == false)
            {
                SecondButtonClicked?.Invoke(_currentPlayer, _costToBuy, UiHash.AdButtonIndex);
                _timer.StartСountDown();
            }
            else
            {
                Debug.Log("сейчас кулдаун");
                StartCoroutine(_popupPanel.Show());
            }
        }

        public void OnExtraButtonClicked()
        {
            ExtraButtonClicked?.Invoke();
            Close();
        }
    }
}