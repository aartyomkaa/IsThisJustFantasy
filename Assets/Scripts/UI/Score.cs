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

        private PlayerWallet _wallet;
        private int _scoreMultiplier = 2;

        public event Action<int> ScoreChanged;

        public void Init(PlayerWallet wallet)
        {
            _wallet = wallet;

            LoadScore();
            ScoreChanged?.Invoke(_totalScore);
        }

        public void ShowResult()
        {
            IncreaseTotalScore();

            SetScore();
        }

        public void SetScore()
        {
            SaveScore(_totalScore);
            SaveLeaderboardScore(_totalScore);
        }

        private void IncreaseTotalScore()
        {
            _totalScore += _wallet.Coins * _scoreMultiplier; 

            ScoreChanged?.Invoke(_totalScore);
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