using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class LoginPanel : Screen
    {
        [SerializeField] private Button _accept;
        [SerializeField] private Button _decline;
        //[SerializeField] private Score _score;

        public event Action Decline;
        public event Action Accept;

        private void OnEnable()
        {
            _accept.onClick.AddListener(OnAccept);
            _decline.onClick.AddListener(OnDecline);
        }

        private void OnDisable()
        {
            _accept.onClick.RemoveListener(OnAccept);
            _decline.onClick.RemoveListener(OnDecline);
        }

        private void OnAccept()
        {
            // PlayerAccount.Authorize();  закомментил ДО билда, в билде раскомментить

            // _score.SetScore(); закомментил ДО билда, в билде раскомментить

            // Accept?.Invoke(); закомментил ДО билда, в билде раскомментить
            Close();
        }

        private void OnDecline()
        {
            // Decline?.Invoke(); закомментил ДО билда, в билде раскомментить
            Close();
        }
    }
}
