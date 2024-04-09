using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BuildingSystem
{
    internal class BuilderButton : MonoBehaviour
    {
        [SerializeField] private Button _build;     

        private PlayerWallet _currentPlayersWallet;

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

            _build.gameObject.SetActive(isPlayerIn);
            SetButtonText(_build, BuildingUiHash.BuildButtonText, costToBuy); 
        }
    }
}