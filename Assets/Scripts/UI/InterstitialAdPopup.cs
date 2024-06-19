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

        private WaitForSeconds _timeToShow;

        private void Start()
        {
            _timeToShow = new WaitForSeconds(3);
        }

        public IEnumerator Show()  
        {
            _popupPanel.SetActive(true);

            yield return _timeToShow;              

            _popupPanel.SetActive(false);
        }
    }
}