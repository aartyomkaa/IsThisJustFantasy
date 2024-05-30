using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Audio;

namespace Assets.Scripts.YandexSDK
{
    internal abstract class AdShower : MonoBehaviour
    {
        private AudioMixer _audioMixer;
        private List<Button> _adButtons;

        private bool _isCurrentSoundOff;

        public abstract void Show();

        public void Init(AudioMixer mixer)
        {
            _audioMixer = mixer;
        }

        protected void OnOpenCallBack()
        {
            Time.timeScale = 0;

            _isCurrentSoundOff = _audioMixer.IsMuted;

            foreach (Button button in _adButtons)
                button.interactable = false;

            if (!_isCurrentSoundOff)
            {
                _audioMixer.Mute();
            }               
        }

        protected void OnCloseCallBack()
        {
            Time.timeScale = 1;

            foreach (Button button in _adButtons)
                button.interactable = true;

            if (!_isCurrentSoundOff)
            {
                _audioMixer.Unmute();
            }   
        }

        protected void OnCloseCallBack(bool wasShown)
        {
            if (wasShown == false)
                return;

            Time.timeScale = 1;

            foreach (Button button in _adButtons)
                button.interactable = true;

            if (!_isCurrentSoundOff)
            {
                _audioMixer.Unmute();
            }
        }
    }
}
