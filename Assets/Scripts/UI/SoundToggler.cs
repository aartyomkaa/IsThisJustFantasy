using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class SoundToggler : MonoBehaviour
    {
        [SerializeField] private Toggle _sound;
        [SerializeField] private Image _ckekMarkOn;
        [SerializeField] private Image _ckekMarkOff;

       
        public event Action <bool> SoundValueChanged;
        
        
        private void OnEnable()
        {
            _sound.onValueChanged.AddListener(OnSoundValueChanged);
        }

        private void OnDisable()
        {
            _sound.onValueChanged.RemoveListener(OnSoundValueChanged);
        }

        private void OnSoundValueChanged(bool isOn)
        {
            if (isOn)
            {
                _sound.graphic = _ckekMarkOn;
                _ckekMarkOn.gameObject.SetActive(true);
                _ckekMarkOff.gameObject.SetActive(false);
                
                SoundValueChanged?.Invoke(isOn);
            }
            else 
            {
                _sound.graphic = _ckekMarkOff;
                _ckekMarkOff.gameObject.SetActive(true);
                _ckekMarkOn.gameObject.SetActive(false);
               
                SoundValueChanged?.Invoke(isOn);
            }         
        }
    }
}