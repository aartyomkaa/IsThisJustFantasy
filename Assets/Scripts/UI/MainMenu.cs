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
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGamesSdk.GameReady();

            switch (YandexGamesSdk.Environment.i18n.lang)
            {
                case "en":
                    LeanLocalization.SetCurrentLanguageAll("English");
                    break;

                case "ru":
                    LeanLocalization.SetCurrentLanguageAll("Russian");
                    break;

                case "tr":
                    LeanLocalization.SetCurrentLanguageAll("Turkish");
                    break;

                default:
                    LeanLocalization.SetCurrentLanguageAll("English");
                    break;
            }

            LeanLocalization.UpdateTranslations();
#endif
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