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

        private SoundToggler _soundToggler;
        private bool _isMuted = false;

        public bool IsMuted => _isMuted;

        public event Action<bool> VolumeValueChanged;

        private void Start()
        {      
            SetVolumeValue();  
        }

        private void OnDisable()
        {
            _soundToggler.SoundValueChanged -= ToggleMusic;
        }

        private void SetVolumeValue()
        {
            float value = PlayerPrefs.GetFloat(PlayerConfigs.MusicVolume);

            if (value == PlayerConfigs.MinVolume)
            {
                Mute();
            }
            else
            {
                Unmute();
            }
        }

        public void SignSoundValuesChanges(SoundToggler soundToggler)
        {
            _soundToggler = soundToggler;
            _soundToggler.SoundValueChanged += ToggleMusic;
        }
     
        public void ToggleMusic(bool isMuted)
        {
            _isMuted = isMuted;

            if (_isMuted)
            {
                Mute();
               
            }
            else
            {
                Unmute();
            } 
        }

        public void Mute()
        {
            _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, PlayerConfigs.MinVolume);
            PlayerPrefs.SetFloat(PlayerConfigs.MusicVolume,PlayerConfigs.MinVolume);
            _isMuted = true;
            VolumeValueChanged?.Invoke(_isMuted);
        }

        public void Unmute()
        {
            _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, PlayerConfigs.MaxVolume);
            PlayerPrefs.SetFloat(PlayerConfigs.MusicVolume, PlayerConfigs.MaxVolume);
            _isMuted = false;
            VolumeValueChanged?.Invoke(_isMuted);
        }
    }
}
