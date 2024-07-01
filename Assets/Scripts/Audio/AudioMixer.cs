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

        public void SignSoundValuesChanges(SoundToggler soundToggler)
        {
            _soundToggler = soundToggler;
            _soundToggler.SoundValueChanged += ToggleMusic;
        }
     
        public void ToggleMusic(bool isMute)
        {
            bool isPlayer = true;

            if (isMute)
            {
                Mute(isPlayer);
               
            }
            else
            {
                Unmute(isPlayer);
            } 
        }

        public void Mute(bool isPlayer = false)
        {
            _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, PlayerConfigs.MinVolume);
            PlayerPrefs.SetFloat(PlayerConfigs.MusicVolume,PlayerConfigs.MinVolume);
            VolumeValueChanged?.Invoke(true);

            if (isPlayer)
                _isMuted = true;
        }

        public void Unmute(bool isPlayer = false)
        {
            _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, PlayerConfigs.MaxVolume);
            PlayerPrefs.SetFloat(PlayerConfigs.MusicVolume, PlayerConfigs.MaxVolume);
            VolumeValueChanged?.Invoke(false);

            if (isPlayer)
                _isMuted = false;
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
    }
}
