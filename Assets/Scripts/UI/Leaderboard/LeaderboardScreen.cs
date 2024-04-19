using Agava.YandexGames;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class LeaderboardScreen : Screen
    {
        [SerializeField] private LoginPanel _loginpanel;
        [SerializeField] private LeaderboardPanel _leaderboardpanel;

        public bool _isPlayerAuthorized;


        private void OnEnable()
        {
            _leaderboardpanel.Closed += OnLeaderBordPanelClosed;
        }

        private void OnDisable()
        {
            _leaderboardpanel.Closed += OnLeaderBordPanelClosed;
        }

        public override void Open()
        {
            base.Open();

            if (_isPlayerAuthorized)
            {
                _leaderboardpanel.Init();
            }
            else
            {
                _loginpanel.Open();
            }
        }

        //public override void Open()                              закомментил ДО билда, в билде раскомментить 
        //{
        //    if (PlayerAccount.IsAuthorized)
        //    {
        //        PlayerAccount.RequestPersonalProfileDataPermission();

        //        _leaderboardpanel.Init();
        //    }
        //    else
        //    {
        //        _loginpanel.Open();
        //    }
        //}

        private void OnLeaderBordPanelClosed()
        {
            Close();
        }
    }
}
