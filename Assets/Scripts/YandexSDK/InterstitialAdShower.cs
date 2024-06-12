using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.YandexSDK
{
    internal class InterstitialAdShower : AdShower
    {
        private float _cooldownTime = 60.5f;
        private float _timeLeft;
        private bool _isOnCooldown;

        public event Action<float> CooldownTimeLeft;

        public override void Show()
        {
            if (_isOnCooldown)
            {
                CooldownTimeLeft?.Invoke(_timeLeft);
            }
            else
            {
                StartCoroutine(Timer());
                InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
            }
        }

        private IEnumerator Timer()
        {
            _isOnCooldown = true;
            _timeLeft = _cooldownTime;

            while (_timeLeft > 0)
            {
                _timeLeft -= Time.fixedDeltaTime;

                yield return Time.fixedDeltaTime;
            }

            _isOnCooldown = false;
        }
    }
}
