using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

namespace Assets.Scripts.UI
{
    internal class MainMenu : MonoBehaviour
    {
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private LevelsPanel _levelsPanel;
        [SerializeField] private Button _openLeaderboardScreenButton;
        [SerializeField] private Button _openLevelsPanelButton;
        [SerializeField] private Button _closeLevelsPanelButton;

        private void Awake()
        {
            YandexGamesSdk.GameReady();
        }

        private void OnEnable()
        {
            _openLeaderboardScreenButton.onClick.AddListener(OpenLeaderboardScreen);
            _openLevelsPanelButton.onClick.AddListener(OpenLevelsPanel);
            _closeLevelsPanelButton.onClick.AddListener(CloseLevelsPanel);
        }

        private void OnDisable()
        {
            _openLeaderboardScreenButton.onClick.RemoveListener(OpenLeaderboardScreen);
            _openLevelsPanelButton.onClick.RemoveListener(OpenLevelsPanel);
            _closeLevelsPanelButton.onClick.RemoveListener(CloseLevelsPanel);
        }

        private void OpenLeaderboardScreen()
        {
            _leaderboardScreen.Open();
        }

        private void OpenLevelsPanel()
        {
            _levelsPanel.Init();
        }

        private void CloseLevelsPanel()
        {
            _levelsPanel.Close();
        }
    }
}