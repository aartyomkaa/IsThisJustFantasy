using System;
using UnityEngine;
using UnityEngine.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.UI;
using Unity.VisualScripting;

namespace Assets.Scripts.Audio
{
    internal class AudioMixer : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixer;
        private SoundToggler _currentSoundToggler;

        private bool _isMuted = false;

        public bool IsMuted => _isMuted;

        public event Action<bool> VolumeValueChanged;

        //public void ChangeVolume(float volume)
        //{
        //    _mixer.audioMixer.SetFloat(PlayerConfigs.MusicVolume, volume);
        //    PlayerPrefs.SetFloat(PlayerConfigs.MusicVolume, volume);

        //    _isMuted = false;
        //    VolumeValueChanged?.Invoke(_isMuted);
        //}

        private void Start()
        {      
            SetVolumeValue();  
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
