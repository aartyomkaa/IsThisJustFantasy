using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.YandexSDK
{
    internal abstract class AdShower : MonoBehaviour
    {
        private List<Button> _buttonsToDeactivate;
        private Pauser _pauser;
        private BackgraoundPauser _backgraoundPauser;

        public abstract void Show();

        public void Init(Pauser pauser, BackgraoundPauser backgraoundPauser)
        {
            _pauser = pauser;
            _backgraoundPauser = backgraoundPauser;
        }

        protected void OnOpenCallBack()
        {
            _backgraoundPauser.gameObject.SetActive(false);
            _pauser.Pause();

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = false;              
        }

        protected void OnCloseCallBack()
        {
            _pauser.Resume();
            _backgraoundPauser.gameObject.SetActive(true);

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = true;  
        }

        protected void OnCloseCallBack(bool wasShown)
        {
            _pauser.Resume();
            _backgraoundPauser.gameObject.SetActive(true);

            if (wasShown == false)
                return;

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = true;
        }
    }
}
