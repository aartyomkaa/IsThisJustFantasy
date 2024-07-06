using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;

namespace Assets.Scripts.YandexSDK
{
    internal abstract class AdShower : MonoBehaviour
    {
        private List<Button> _buttonsToDeactivate;
        private Pauser _pauser;

        public bool IsPaused { get; private set; }

        public abstract void Show();

        public void Init(Pauser pauser)
        {
            _pauser = pauser;
        }

        protected void OnOpenCallBack()
        {
            _pauser.Pause();
            IsPaused = true;

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = false;              
        }

        protected void OnCloseCallBack()
        {
            _pauser.Resume();
            IsPaused = false;

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = true;  
        }

        protected void OnCloseCallBack(bool wasShown)
        {
            _pauser.Resume();
            IsPaused = false;

            if (wasShown == false)
                return;

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = true;
        }
    }
}