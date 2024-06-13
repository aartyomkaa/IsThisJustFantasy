using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;

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
        
        private Player _currentPlayer;
        private bool _isActive;
        private float _changeScaleSpeed = 0.15f;
        private int _panelMoveXValue = 228;

       // private WaitForSeconds _delay;
       // private float _cooldownTime = 60.5f;
       // private bool _isOnCooldown = false;
        private bool _isSecondButtonActive = true;

        public bool IsSecondButtonActive => _isSecondButtonActive;

        public Button SecondButton => _secondButton;

        public event Action<Player, int, int> FirstButtonClicked;
        public event Action<Player, int, int> SecondButtonClicked;
        public event Action ExtraButtonClicked;
        public event Action AddButtonClicked;
        public event Action AddButtonUnavailable;

        private void Start()
        {
           // _delay = new WaitForSeconds(_cooldownTime);
            _cost.SetValue(_costToBuy);
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

        public void OnFirsttButtonClicked()
        {
            FirstButtonClicked?.Invoke(_currentPlayer, _costToBuy, UiHash.CoinsButtonIndex);
        }

        public void OnSecondButtonClicked()
        {
            if (_isSecondButtonActive == true)
            {
                SecondButtonClicked?.Invoke(_currentPlayer, _costToBuy, UiHash.AdButtonIndex);
                DeActivateSecondButton();
                AddButtonClicked?.Invoke();
            }
            else
            {
                AddButtonUnavailable?.Invoke();
            }          
        }

        public void OnExtraButtonClicked()
        {
            ExtraButtonClicked?.Invoke();
            Close();
        }

        public void ActivateSecondButton()
        {
            _isSecondButtonActive = true;
        }

        public void DeActivateSecondButton()
        {
            _isSecondButtonActive = false;
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

        //private IEnumerator Timer()
        //{
        //    _isOnCooldown = true;

        //    yield return _delay;

        //    _isOnCooldown = false;
        //}
    }
}