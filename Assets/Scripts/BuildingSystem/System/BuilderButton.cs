using Assets.Scripts.GameLogic.Utilities;
using Assets.Scripts.PlayerComponents;
using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BuildingSystem
{
    internal class BuilderButton : MonoBehaviour
    {
        [SerializeField] private Button _build;
        [SerializeField] private LeanToken _cost;
        [SerializeField] private LeanToken _building;

        private ButtonTranslator _translator = new ButtonTranslator();
        private PlayerWallet _currentPlayersWallet;
        private Vector3 _closeValues = Vector3.zero;
        private Vector3 _openValues = new Vector3(1,1,1);
        private float _changeScaleSpeed = 0.1f;
        private bool _currentStatus;

        public  Action<PlayerWallet> BuildButtonClicked;

        private void OnEnable()
        {
            _build.onClick.AddListener(OnBuildButtonClicked);
        }

        private void OnDisable()
        {
            _build.onClick.RemoveListener(OnBuildButtonClicked);
        }

        private void SetButtonText(int builPointIndex, int costToBuy)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _building.SetValue(_translator.GetTranslation(builPointIndex));
            _cost.SetValue(costToBuy);
#endif
        }
           
        private void OnBuildButtonClicked()
        {
            BuildButtonClicked?.Invoke(_currentPlayersWallet);   
        }

        public void ToggleButton(PlayerWallet wallet, int builPointIndex, int costToBuy, bool isPlayerIn)   
        {
            _currentPlayersWallet = wallet;
            _currentStatus = isPlayerIn;

            if (isPlayerIn)
            {
                Open();
                SetButtonText(builPointIndex, costToBuy);
                _build.gameObject.SetActive(isPlayerIn);
            }
            else
            {
                Close();
            }
        }

        private void Close()
        {
            LeanTween.scale(_build.gameObject, _closeValues, _changeScaleSpeed).setOnComplete(ChangeStatus);
        }
        private void Open()
        {
            LeanTween.scale(_build.gameObject, _openValues, _changeScaleSpeed); 
        }

        private void ChangeStatus()
        {
            _build.gameObject.SetActive(_currentStatus);
        }
    }
}