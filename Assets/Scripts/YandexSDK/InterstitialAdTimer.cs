using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.BuildingSystem.Buildings;
using Agava.YandexGames;
using System;

namespace Assets.Scripts.YandexSDK
{
    internal class InterstitialAdTimer : MonoBehaviour
    {
        private InterstitialAdShower _interstitialAd;
       // private List<ColliderPanelEventer> _eventers;
       // private float _cooldown = 60.5f;
        private WaitForSeconds _cooldown; 

        
        //private float _timeLeft;
        private bool _isOnCooldown;
        

        public bool IsOnCooldown => _isOnCooldown;

        public event Action<bool> CooldownStarted;
        public event Action<bool> BecomeAvailable;


        private void Start()
        {
            _cooldown = new WaitForSeconds(60.5f);
        }

        //private void OnDisable()
        //{
        //    UnSignToAdButtons();
        //}

        public void Init(InterstitialAdShower interstitialAd)
        {
            _interstitialAd = interstitialAd;
            //_eventers = eventers;
        }



        private IEnumerator —ountDown()
        {
            yield return _cooldown;
        }

        public void Start—ountDown()
        {
            _isOnCooldown = true;
            CooldownStarted?.Invoke(_isOnCooldown);
            _interstitialAd.Show();
            StartCoroutine(—ountDown());
            _isOnCooldown = false;
            BecomeAvailable?.Invoke(_isOnCooldown);
        }

        //public void AddEventer(ColliderPanelEventer eventer)
        //{
        //    _eventers.Add(eventer);
        //}


        //private void TryToAllowShowAd()    
        //{
        //    for (int i = 0; i < _eventers.Count; i++)
        //    {
        //        if (_eventers[i].IsSecondButtonActive == true)
        //        {
        //            _interstitialAd.Show();
        //            TemporarilyDeActivateAllAddButtons();
        //            StartCoroutine(—ountDown());
        //            ActivateAllAddButtons();
        //        }    
        //    }  
        //}

        //private void TemporarilyDeActivateAllAddButtons()
        //{
        //    for (int i = 0; i < _eventers.Count; i++)
        //    {
        //        _eventers[i].DeActivateSecondButton();
        //    }
        //}

        //private void ActivateAllAddButtons()
        //{
        //    for (int i = 0; i < _eventers.Count; i++)
        //    {
        //        _eventers[i].ActivateSecondButton();
        //    }
        //}


        //public void SignToAdButtons()
        //{
        //    for (int i = 0; i < _eventers.Count; i++)
        //    {
        //        _eventers[i].AddButtonClicked += TryToAllowShowAd;
        //    }
        //}

        //public void UnSignToAdButtons()
        //{
        //    for (int i = 0; i < _eventers.Count; i++)
        //    {
        //        _eventers[i].AddButtonClicked -= TryToAllowShowAd;
        //    }
        //}


    }
}