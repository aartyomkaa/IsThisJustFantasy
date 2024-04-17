using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class SoundToggler : MonoBehaviour
    {
        [SerializeField] private Sprite _ckekMarkOn;
        [SerializeField] private Sprite _ckekMarkOff;
        [SerializeField] private Button _soundButton;

        private bool _isMuted;

        public event Action <bool> SoundValueChanged;    
        
        private void OnEnable()
        {
            _soundButton.onClick.AddListener(OnSoundValueChanged); 
        }

        private void OnDisable()
        {
            _soundButton.onClick.RemoveListener(OnSoundValueChanged);
        }

        private void OnSoundValueChanged()
        {
            if (_isMuted == true)
            {   
                TurnOn();
            }
            else
            {
                Turnoff();   
            }
           
            SoundValueChanged?.Invoke(_isMuted);
            SetCurrentImage();
        }

        private void TurnOn()
        {
            _isMuted = false;
        }

        private void Turnoff()
        {
            _isMuted = true;
        }

        public void SetCurrentStatus(bool isMuted)
        {
            _isMuted = isMuted;
            SetCurrentImage();
        }

        private void SetCurrentImage()
        {
            if(_isMuted)
            {
                _soundButton.image.sprite = _ckekMarkOff;
            }
            else
            {
                _soundButton.image.sprite = _ckekMarkOn;
            }
        }
    }
}