using Assets.Scripts.Constants;
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

        private void SetButtonText(Button activeButton, int builPointIndex, int costToBuy)
        {
            switch (builPointIndex)
            {
                case BuildingsHash.TowerIndex:
                    _building.SetValue(UiHash.BuildTowerButtonText);
                    _cost.SetValue(costToBuy);     
                    break;

                case BuildingsHash.BarracksIndex:
                    _building.SetValue(UiHash.BuildBarracksButtonText);
                    _cost.SetValue(costToBuy);
                    break;

                case BuildingsHash.ResoorceBuildingIndex:
                    _building.SetValue(UiHash.BuildResoorceBuildingButtonText);
                    _cost.SetValue(costToBuy);
                    break;

                default:
                    throw new Exception("No such building id");
            }
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
                SetButtonText(_build, builPointIndex, costToBuy);
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