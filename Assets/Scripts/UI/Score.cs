using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using System;
using UnityEngine;
using Agava.YandexGames;
using Agava.WebUtility;

namespace Assets.Scripts.UI
{
    internal class Score : MonoBehaviour
    {
        private int _totalScore;
        private int _levelScore;

        private PlayerWallet _wallet;
        private int _scoreMultiplier = 3;

        public int TotalScore => _totalScore;
        public int LevelScore => _levelScore;

        public void Init(PlayerWallet wallet)
        {
            _wallet = wallet;

            LoadScore();
        }

        public void UpdateTotalScore()
        {
            IncreaseTotalScore();

            SaveScore(_totalScore);
            SaveLeaderboardScore(_totalScore);
        }

        public void UpdateLevelScore()
        {
            _levelScore += _wallet.Coins * _scoreMultiplier;
        }

        private void IncreaseTotalScore()
        {
            _totalScore += _levelScore;
        }

        private void LoadScore()
        {
            if (PlayerPrefs.HasKey(PlayerConfigs.Score))
            {
                _totalScore = PlayerPrefs.GetInt(PlayerConfigs.Score);
            }
        }

        private void SaveScore(int value)
        {
            PlayerPrefs.SetInt(PlayerConfigs.Score, value);
            PlayerPrefs.Save();
        }

        private void SaveLeaderboardScore(int value)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
            {
                Agava.YandexGames.Leaderboard.GetPlayerEntry(PlayerConfigs.Leaderboard, response =>
                {
                    Agava.YandexGames.Leaderboard.SetScore(PlayerConfigs.Leaderboard, value);
                });
            }
#endif
        }
    }
}