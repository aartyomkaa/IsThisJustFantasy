using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.Audio;
using Agava.WebUtility;

namespace Assets.Scripts.GameLogic
{
    internal class BackgraoundPauser : MonoBehaviour
    {
        private Pauser _pauser;
        private AudioMixer _mixer;

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
            Application.focusChanged += OnInBackgroundChangeApp;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
            Application.focusChanged -= OnInBackgroundChangeApp;
        }

        public void Init(Pauser pauser, AudioMixer mixer)
        {
            _pauser = pauser;
            _mixer = mixer;
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (isBackground)
            {
                _mixer.Mute();
                _pauser.Pause();
            }
            else
            {
                _mixer.Unmute();
                _pauser.Resume();
            }
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (!inApp)
            {
                _mixer.Mute();
                _pauser.Pause();
            }
            else
            {
                _mixer.Unmute();
                _pauser.Resume();
            }
        }
    }
}