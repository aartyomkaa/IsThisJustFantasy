using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BuildingSystem
{
    internal class BuilderButton : MonoBehaviour
    {
        [SerializeField] private Button _build;     

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

        private void SetButtonText(Button activeButton,string title, int costToBuy)
        {
            activeButton.GetComponentInChildren<TMP_Text>().text = title + costToBuy;
        }

        private void OnBuildButtonClicked()
        {
            BuildButtonClicked?.Invoke(_currentPlayersWallet);   
        }

        public void ToggleButton(PlayerWallet wallet, int costToBuy, bool isPlayerIn)   
        {
            _currentPlayersWallet = wallet;
            _currentStatus = isPlayerIn;

            if (isPlayerIn)
            {
                Open();
                _build.gameObject.SetActive(isPlayerIn);
            }
            else
            {
                Close();
            }
                 
            SetButtonText(_build, BuildingUiHash.BuildButtonText, costToBuy); 
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