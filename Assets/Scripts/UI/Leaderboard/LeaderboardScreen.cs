using Agava.YandexGames;
using UnityEngine;

namespace Assets.Scripts.UI.Leaderboard
{
    internal class LeaderboardScreen 
    {
        [SerializeField] private GameObject _loginpanel;
       

        public  void Open()
        {
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
        
            }
            else
            {
                _loginpanel.SetActive(true);
            }
        }
    }
}
