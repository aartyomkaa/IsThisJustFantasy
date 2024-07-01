﻿using UnityEngine;
using Assets.Scripts.UI;
using Agava.WebUtility;
using Assets.Scripts.YandexSDK;


namespace Assets.Scripts.GameLogic
{
    internal class BackgraoundPauser : MonoBehaviour
    {
        private Pauser _pauser;
        private PausePanel _pausePanel;
        private VideoAdShower _videoAd;
        private InterstitialAdShower _interstitialAd;
        private bool _isOnPause;

        private void OnDisable()
        {
            _pausePanel.PauseStatusChanged -= ChangeCapableToPause;
            _videoAd.PauseStatusChanged -= ChangeCapableToPause;
            _interstitialAd.PauseStatusChanged -= ChangeCapableToPause;

            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
          //  Application.focusChanged -= OnInBackgroundChangeApp;
        }

        public void Init(Pauser pauser, PausePanel pausePanel, VideoAdShower videoAd, InterstitialAdShower interstitialAd)
        {
            _pauser = pauser;
            _pausePanel = pausePanel;
            _videoAd = videoAd;
            _interstitialAd = interstitialAd;

            _pausePanel.PauseStatusChanged += ChangeCapableToPause;
            _videoAd.PauseStatusChanged += ChangeCapableToPause;
            _interstitialAd.PauseStatusChanged += ChangeCapableToPause;

            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
           // Application.focusChanged += OnInBackgroundChangeApp;
        }

       
        private void ChangeCapableToPause(bool isAllowedToPause)
        {
            _isOnPause = isAllowedToPause;
        }
        

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (!_isOnPause)
            {
                if (isBackground)
                {
                    _pauser.Pause();
                }
                else
                {
                    _pauser.Resume();
                }
            }
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (!_isOnPause)
            {
                if (!inApp)
                {
                    _pauser.Pause();
                }
                else
                {
                    _pauser.Resume();
                }
            }
        }
    }
}