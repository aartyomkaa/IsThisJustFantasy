using UnityEngine;
using Assets.Scripts.Audio;
using Assets.Scripts.PlayerInput;

namespace Assets.Scripts.UI
{
    internal class Pauser
    {
        private AudioMixer _audioMixer;
        private MobileInput _mobileInput;
        private bool _isCurrentSoundOff;
        private bool _isOnPause;

        public bool IsOnPause => _isOnPause;

        public Pauser(AudioMixer audioMixer, MobileInput mobileInput)
        {
            _audioMixer = audioMixer;
            _mobileInput = mobileInput;
        }

        public void Pause()
        {
            _isCurrentSoundOff = _audioMixer.IsMuted;
            _isOnPause = true;

            if (_mobileInput != null)
            {
                _mobileInput.SetVisibility(false);
            }

            if (!_isCurrentSoundOff)
            {
                _audioMixer.Mute();
            }

            Time.timeScale = 0;
        }

        public void Resume()
        {
            _isOnPause = false;
            
            if (_mobileInput != null)
             {
                    _mobileInput.SetVisibility(true);
             }

             if (!_isCurrentSoundOff)
             {
                    _audioMixer.Unmute();
             }

             Time.timeScale = 1;
 
        }
    }
}