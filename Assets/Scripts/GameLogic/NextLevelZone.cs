using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal class NextLevelZone : MonoBehaviour
    {
        [SerializeField] private ScorePanel _nextLevelPanel;

        private Score _score;
        private SceneLoader _sceneLoader;
        private Player _player;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _nextLevelPanel.BackButtonPressed += OnBackButtonPressed;
            _nextLevelPanel.ContinueButtonPressed += OnContinueButtonPressed;
        }

        private void OnDisable()
        {
            _nextLevelPanel.BackButtonPressed -= OnBackButtonPressed;
            _nextLevelPanel.ContinueButtonPressed -= OnContinueButtonPressed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                OpenNextLevelPanel();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _nextLevelPanel.gameObject.SetActive(false);
            }
        }

        public void Init(Score score, SceneLoader sceneLoader, Player player)
        {
            _score = score;
            _sceneLoader = sceneLoader;
        }

        public void OnAllWavesDefeated()
        {
            gameObject.SetActive(true);
        }

        private void OpenNextLevelPanel()
        {
            _nextLevelPanel.SetTextScore(_score.GetLevelScore().ToString());
            _nextLevelPanel.gameObject.SetActive(true);
        }

        private void OnContinueButtonPressed()
        {
            _player.LevelUp();
            _nextLevelPanel.gameObject.SetActive(false);
            _score.UpdateTotalScore();
            _sceneLoader.LoadNextScene();
        }

        private void OnBackButtonPressed()
        {
            _nextLevelPanel.gameObject.SetActive(false);
        }
    }
}