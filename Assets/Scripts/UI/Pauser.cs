using UnityEngine;
using Assets.Scripts.Audio;
using Assets.Scripts.PlayerInput;

namespace Assets.Scripts.UI
{
    internal class Pauser
    {
        private PausePanel _pausePanel;
        private AudioMixer _audioMixer;
        private MobileInput _mobileInput;

        public Pauser(AudioMixer audioMixer, MobileInput mobileInput, PausePanel pausePanel)
        {
            _audioMixer = audioMixer;
            _mobileInput = mobileInput;
            _pausePanel = pausePanel;
        }

        public void Pause()
        { 
            if (_mobileInput != null)
            {
                _mobileInput.SetVisibility(false);
            }

            if (_audioMixer.IsMuted == false)
            {
                _audioMixer.Mute();
            }

            Time.timeScale = 0;
        }

        public void Resume()
        {
            if (_pausePanel.IsOpen == true)
                return;

            if (_mobileInput != null)
            {
                _mobileInput.SetVisibility(true);
            }

            if (_audioMixer.IsMuted == false)
            {
                _audioMixer.Unmute();
            }

            Time.timeScale = 1;
        }
    }
}