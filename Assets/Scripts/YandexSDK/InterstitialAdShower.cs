using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.YandexSDK
{
    internal class InterstitialAdShower : AdShower
    {
        public override void Show()
        {
            Debug.Log("Реклама включилась");
            //InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
        }
    }
}
