using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;
using System;

namespace Assets.Scripts.YandexSDK
{
    internal abstract class AdShower : MonoBehaviour
    {
        private List<Button> _buttonsToDeactivate;
        private Pauser _pauser;
        private bool _isPaused;

        public event Action<bool> ChangedPauseStatus;

        public abstract void Show();

        public void Init(Pauser pauser)
        {
            _pauser = pauser;
        }

        protected void OnOpenCallBack()
        {
            _pauser.Pause();
            _isPaused = true;
            ChangedPauseStatus?.Invoke(_isPaused);

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = false;              
        }

        protected void OnCloseCallBack()
        {
            _pauser.Resume();
            _isPaused = false;
            ChangedPauseStatus?.Invoke(_isPaused);

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = true;  
        }

        protected void OnCloseCallBack(bool wasShown)
        {
            _pauser.Resume();
            _isPaused = false;
            ChangedPauseStatus?.Invoke(_isPaused);

            if (wasShown == false)
                return;

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = true;
        }
    }
}
