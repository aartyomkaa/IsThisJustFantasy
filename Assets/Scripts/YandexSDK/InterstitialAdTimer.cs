using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.YandexSDK
{
    internal class InterstitialAdTimer : MonoBehaviour
    {
        private InterstitialAdShower _interstitialAd;
        private WaitForSeconds _cooldown; 
        private bool _isOnCooldown;
        
        public bool IsOnCooldown => _isOnCooldown;

        public event Action<bool> CooldownStarted;
        public event Action<bool> BecomeAvailable;

        private void Start()
        {
            _cooldown = new WaitForSeconds(60.5f);
        }

        private IEnumerator —ountDown()
        {
            yield return _cooldown;

            _isOnCooldown = false;
            BecomeAvailable?.Invoke(_isOnCooldown);
        }

        public void Init(InterstitialAdShower interstitialAd)
        {
            _interstitialAd = interstitialAd;
        }

        public void Start—ountDown()
        {
            _isOnCooldown = true;
            CooldownStarted?.Invoke(_isOnCooldown);
            _interstitialAd.Show();
            StartCoroutine(—ountDown());     
        }
    }
}