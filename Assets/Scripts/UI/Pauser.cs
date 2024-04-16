using Assets;
using Assets.Scripts.Audio;
using Assets.Scripts.PlayerInput;
using UnityEngine;


namespace Assets.Scripts.UI
{
    internal class Pauser
    {
        private AudioMixer _audioMixer;
        private MobileInput _mobileInput;

        public Pauser(AudioMixer audioMixer, MobileInput mobileInput)
        {
            _audioMixer = audioMixer;
            _mobileInput = mobileInput;
        }

        public void Pause()
        {
            if (_mobileInput != null && Application.isMobilePlatform)
            {
              _mobileInput.gameObject.SetActive(false);
            }

            if (_audioMixer != null)
            {
                _audioMixer.Mute();
            }

            Time.timeScale = 0;
        }

        public void Resume()
        {
            if (_mobileInput != null && Application.isMobilePlatform)
            {
                _mobileInput.gameObject.SetActive(true);
            }

            if (_audioMixer != null)
            {
                _audioMixer.Unmute();
            }

            Time.timeScale = 1;
        }
    }
}