﻿using Assets.Scripts.Constants;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class LeaderboardPanel : Screen
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private LeaderboardView _leaderboardView;

        private int _topPlayers = 5;

        public event Action Closed;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnClose);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnClose);
        }

        public void Init()
        {
            Open();

            // ClearViews(); закомментил ДО билда, в билде раскомментить

            // LoadEntries();  закомментил ДО билда, в билде раскомментить
        }

        private void LoadEntries()
        {
            Agava.YandexGames.Leaderboard.GetEntries(PlayerConfigs.Leaderboard, (result) =>
            {
                for (int i = 0; i < _topPlayers; i++)
                {
                    _leaderboardView.Create(result.entries[i]);
                }
            });
        }

        private void ClearViews()
        {
            _leaderboardView.Clear();
        }

        private void OnClose()
        {
           // ButtonAudio.Play();
            Closed?.Invoke();
            Close();
        }
    }
}
