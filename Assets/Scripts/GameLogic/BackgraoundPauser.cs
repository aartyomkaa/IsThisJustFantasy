using UnityEngine;
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
        private bool _cantPause;

        private void OnDisable()
        {
            _pausePanel.ChangedPauseStatus -= ChangeCapableToPause;
            _videoAd.ChangedPauseStatus -= ChangeCapableToPause;
            _interstitialAd.ChangedPauseStatus -= ChangeCapableToPause;

            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
            Application.focusChanged -= OnInBackgroundChangeApp;
        }

        public void Init(Pauser pauser, PausePanel pausePanel, VideoAdShower videoAd, InterstitialAdShower interstitialAd)
        {
            _pauser = pauser;
            _pausePanel = pausePanel;
            _videoAd = videoAd;
            _interstitialAd = interstitialAd;

            _pausePanel.ChangedPauseStatus += ChangeCapableToPause;
            _videoAd.ChangedPauseStatus += ChangeCapableToPause;
            _interstitialAd.ChangedPauseStatus += ChangeCapableToPause;

            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
            Application.focusChanged += OnInBackgroundChangeApp;
        }

       
        private void ChangeCapableToPause(bool cantPause)
        {
            _cantPause = cantPause;
        }
        

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (!_cantPause)
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
            if (!_cantPause)
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