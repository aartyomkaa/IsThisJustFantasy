using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal abstract class PanelToungle : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _panel;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpenButtonClicked);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClicked);
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void OnOpenButtonClicked()
        {
            _panel.SetActive(true);
        }

        public void OnCloseButtonClicked()
        {
            _panel.SetActive(false);
        }
    }
}