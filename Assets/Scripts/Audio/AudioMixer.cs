using System;
using UnityEngine;
using UnityEngine.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.UI;

namespace Assets.Scripts.Audio
{
    internal class AudioMixer : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixer;

        private float _minVolume = -80;
        private float _maxVolume = 0;
        private SoundToggler _currentSoundToggler;

        private bool _isMuted = false;

        public bool IsMuted => _isMuted;

        public event Action<bool> Muted;

        public void ChangeVolume(float volume)
        {
            _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, volume);
            PlayerPrefs.SetFloat(PlayerConfigs.MusicVolume, volume);

            _isMuted = false;
            Muted?.Invoke(_isMuted);
        }

        public void SignSoundValuesChanges(SoundToggler soundToggler)
        {
            _currentSoundToggler = soundToggler;
            _currentSoundToggler.SoundValueChanged += ToggleMusic;
        }

        private void OnDisable()
        {
            _currentSoundToggler.SoundValueChanged -= ToggleMusic;
        }

        public void ToggleMusic(bool isOn)
        {
            _isMuted = isOn;

            if (_isMuted)
            {
                SetVolumeValue();

                _isMuted = false;
            }
            else
            {
                _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, _minVolume);
                _isMuted = true;
            }

            Muted?.Invoke(_isMuted);
        }

        public void Mute()
        {
            if (_isMuted == false)
                _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, _minVolume);
        }

        public void Unmute()
        {
            if (_isMuted == false)
            {
                SetVolumeValue();
            }
        }

        private void SetVolumeValue()
        {
            float value = PlayerPrefs.GetFloat(PlayerConfigs.MusicVolume);

            if (value > _minVolume)
                _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, value);
            else
                _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, _maxVolume);
        }
    }
}
