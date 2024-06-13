using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.BuildingSystem.Buildings;
using Agava.YandexGames;

namespace Assets.Scripts.YandexSDK
{
    internal class InterstitialAdTimer : MonoBehaviour
    {
        private InterstitialAdShower _interstitialAd;
        private List<ColliderPanelEventer> _eventers;
       // private float _cooldown = 60.5f;
        private WaitForSeconds _cooldown; 

        
        private float _timeLeft;
        private bool _isOnCooldown;

        public bool IsOnCooldown => _isOnCooldown;


        private void Start()
        {
            _cooldown = new WaitForSeconds(60.5f);
        }

        private void OnDisable()
        {
            UnSignToAdButtons();
        }

        public void Init(InterstitialAdShower interstitialAd)
        {
            _interstitialAd = interstitialAd;
            //_eventers = eventers;
        }

        public void AddEventer(ColliderPanelEventer eventer)
        {
            _eventers.Add(eventer);
        }

        private IEnumerator ÑountDown()
        {
            yield return _cooldown;
        }

        private void TryToAllowShowAd()    
        {
            for (int i = 0; i < _eventers.Count; i++)
            {
                if (_eventers[i].IsSecondButtonActive == true)
                {
                    _interstitialAd.Show();
                    TemporarilyDeActivateAllAddButtons();
                    StartCoroutine(ÑountDown());
                    ActivateAllAddButtons();
                }    
            }  
        }

        private void TemporarilyDeActivateAllAddButtons()
        {
            for (int i = 0; i < _eventers.Count; i++)
            {
                _eventers[i].DeActivateSecondButton();
            }
        }

        private void ActivateAllAddButtons()
        {
            for (int i = 0; i < _eventers.Count; i++)
            {
                _eventers[i].ActivateSecondButton();
            }
        }


        public void SignToAdButtons()
        {
            for (int i = 0; i < _eventers.Count; i++)
            {
                _eventers[i].AddButtonClicked += TryToAllowShowAd;
            }
        }

        public void UnSignToAdButtons()
        {
            for (int i = 0; i < _eventers.Count; i++)
            {
                _eventers[i].AddButtonClicked -= TryToAllowShowAd;
            }
        }


    }
}