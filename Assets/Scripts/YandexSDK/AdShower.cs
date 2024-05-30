using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;

namespace Assets.Scripts.YandexSDK
{
    internal abstract class AdShower : MonoBehaviour
    {
        private List<Button> _adButtons;
        private Pauser _pauser;

        public abstract void Show();

        public void Init(Pauser pauser)
        {
            _pauser = pauser;
        }

        protected void OnOpenCallBack()
        {
            _pauser.Pause();

            foreach (Button button in _adButtons)
                button.interactable = false;              
        }

        protected void OnCloseCallBack()
        {
            _pauser.Resume();

            foreach (Button button in _adButtons)
                button.interactable = true;  
        }

        protected void OnCloseCallBack(bool wasShown)
        {
            _pauser.Resume();

            if (wasShown == false)
                return;

            foreach (Button button in _adButtons)
                button.interactable = true;
        }
    }
}
