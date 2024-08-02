using UnityEngine;
using Agava.YandexGames;

namespace Assets.Scripts.Yandex
{
    public class SDKMetricStart : MonoBehaviour
    {
        private void Start()
        {
            OnCallGameReadyButtonClick();
        }

        public void OnCallGameReadyButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGamesSdk.GameReady();
#endif
        }
    }
}