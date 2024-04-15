using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class GlobalUI : MonoBehaviour
    {
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private SoundToggler _soundToggler;

        public PlayerUI PlayerUI => _playerUI;
        public PausePanel PausePanel => _pausePanel;
        public SoundToggler SoundToggler => _soundToggler;
    }
}