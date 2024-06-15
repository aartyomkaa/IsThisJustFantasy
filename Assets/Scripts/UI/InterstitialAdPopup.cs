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

        private WaitForSeconds _timeToShow;

        private void Start()
        {
            _timeToShow = new WaitForSeconds(3);
        }

        private void OnDisable()
        {

        }

        public void Init(InterstitialAdShower interstitialAd)
        {
            //����������� �� ������� interstitialAd, ������ ��������� float
        }

        public IEnumerator Show()  //�������� ��������
        {
            _popupPanel.SetActive(true);

            yield return _timeToShow;              //new WaitForSeconds(_timeToShow); 

            _popupPanel.SetActive(false);
        }
    }
}