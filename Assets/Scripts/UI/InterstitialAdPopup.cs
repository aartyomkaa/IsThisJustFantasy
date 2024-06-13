using Assets.Scripts.EnemyComponents;
using Assets.Scripts.YandexSDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class InterstitialAdPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popupPanel;
        [SerializeField] private TMP_Text _secondsUntilAvailable;

        private float _timeToShow = 3;


        private void OnDisable()
        {

        }

        public void Init(InterstitialAdShower interstitialAd)
        {
            //подписаться на событие interstitialAd, должно принимать float
        }

        private IEnumerator ShowPanel()  //изменить название
        {
            _popupPanel.SetActive(true);

             yield return new WaitForSeconds(_timeToShow); 

            _popupPanel.SetActive(false);
        }
    }
}