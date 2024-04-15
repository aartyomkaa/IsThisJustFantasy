using Assets;
using Assets.Scripts.PlayerInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

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
            if (_mobileInput != null)
            {
                if (Application.isMobilePlatform)
                {
                _mobileInput.gameObject.SetActive(false);
                }
            }

            if (_audioMixer != null)
            {
                    _audioMixer.Mute();
                    Time.timeScale = 0;
            }   
    }
}
