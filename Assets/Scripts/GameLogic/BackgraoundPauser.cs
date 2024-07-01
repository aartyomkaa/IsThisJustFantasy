using UnityEngine;
using Assets.Scripts.UI;
using Agava.WebUtility;

namespace Assets.Scripts.GameLogic
{
    internal class BackgraoundPauser : MonoBehaviour
    {
        private Pauser _pauser;

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

        public void Init(Pauser pauser)
        {
            _pauser = pauser;
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
                if (isBackground)
                {
                    _pauser.Pause();
                }
                else
                {
                    _pauser.Resume();
                }
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
                if (!inApp)
                {
                    _pauser.Pause();
                }
                else
                {
                    _pauser.Resume();
                }
        }
    }
}