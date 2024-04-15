using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.YandexSDK
{
    internal abstract class AdShower : MonoBehaviour
    {
        [SerializeField] private List<Button> _adButtons;
        [SerializeField] private AudioMixer _audioMixer;

        public abstract void Show();

        protected void OnOpenCallBack()
        {
            Time.timeScale = 0;

            foreach (Button button in _adButtons)
                button.interactable = false;

            _audioMixer.Mute();
        }

        protected void OnCloseCallBack()
        {
            Time.timeScale = 1;

            foreach (Button button in _adButtons)
                button.interactable = true;

            _audioMixer.Unmute();
        }

        protected void OnCloseCallBack(bool wasShown)
        {
            if (wasShown == false)
                return;

            Time.timeScale = 1;

            foreach (Button button in _adButtons)
                button.interactable = true;

            _audioMixer.Unmute();
        }
    }
}
