using System;
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
        [SerializeField] private Button _firstButton;    //+ один выброс за игровые деньги   
        [SerializeField] private Button _secondButton;   // - минус один выброс за рекламу
        [SerializeField] private Button _extraButton;   // кнопка начала новой волны, пока не знаю как назвать
        [SerializeField] private TutorialPanel _tutorial;
        [SerializeField] private LeanToken _cost;
        
        private Player _currentPlayer;
        private bool _currentStatus;
        private float _changeScaleSpeed = 0.15f;
        private int _panelMoveXValue = 228;

        public Button SecondButton => _secondButton;

        public Action<Player, int, int> FirstButtonClicked;
        public Action<Player, int,int> SecondButtonClicked;
        public Action ExtraButtonClicked;

        private void Start()
        {
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
                _currentStatus = true;
                Open();
                _currentPlayer = player;
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

        public void OnFirsttButtonClicked()
        {
            FirstButtonClicked?.Invoke(_currentPlayer, _costToBuy, UiHash.CoinsButtonIndex);
        }

        public void OnSecondButtonClicked()
        {
            SecondButtonClicked?.Invoke(_currentPlayer, _costToBuy, UiHash.AdButtonIndex);
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

            if (PlayerPrefs.GetInt(PlayerConfigs.HasPassedTutorial) == 0)
                _tutorial.Open();
        }

        private void Close()
        {
            LeanTween.moveX(_panelToShow.GetComponent<RectTransform>(), _panelMoveXValue, _changeScaleSpeed).setOnComplete(ChangeActiveStatus);

            if (PlayerPrefs.GetInt(PlayerConfigs.HasPassedTutorial) == 0)
                _tutorial.Close();
        }

        private void ChangeActiveStatus()
        {
            _panelToShow.gameObject.SetActive(_currentStatus);
        }
    }
}