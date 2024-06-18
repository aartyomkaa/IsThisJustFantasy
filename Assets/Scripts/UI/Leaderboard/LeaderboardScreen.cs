using UnityEngine;
using Agava.YandexGames;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class LeaderboardScreen : Screen
    {
        [SerializeField] private LoginPanel _loginpanel;
        [SerializeField] private LeaderboardPanel _leaderboardpanel;
        [SerializeField] private Button _close;

        private void OnEnable()
        {
            _close.onClick.AddListener(Close);

            _leaderboardpanel.Closed += Close;
            _loginpanel.Accept += Close;
            _loginpanel.Decline += Close;
        }

        private void OnDisable()
        {
            _close.onClick.RemoveListener(Close);

            _leaderboardpanel.Closed -= Close;
            _loginpanel.Accept -= Close;
            _loginpanel.Decline -= Close;
        }

        public override void Open()
        {
            base.Open();

            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();

                _leaderboardpanel.Init();
            }
            else
            {
                _loginpanel.Open();
            }
        }
    }
}