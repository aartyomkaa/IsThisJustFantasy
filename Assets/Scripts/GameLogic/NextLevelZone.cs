using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Assets.Scripts.GameLogic
{
    internal class NextLevelZone : MonoBehaviour
    {
        private ScorePanel _nextLevelPanel;

        private Score _score;
        private SceneLoader _sceneLoader;
        private Player _player;
        private Pauser _pauser;

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

        public void Init(Score score, SceneLoader sceneLoader, Player player, Pauser pauser, ScorePanel nextLevelPanel)
        {
            _pauser = pauser;
            _nextLevelPanel = nextLevelPanel;
            _player = player;

            _nextLevelPanel.BackButtonPressed += OnBackButtonPressed;
            _nextLevelPanel.ContinueButtonPressed += OnContinueButtonPressed;

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
            _pauser.Pause();
        }

        private void OnContinueButtonPressed()
        {
            _player.LevelUp();
            _nextLevelPanel.gameObject.SetActive(false);
            _score.UpdateTotalScore();
            _pauser.Resume();
            _sceneLoader.LoadNextScene();
        }

        private void OnBackButtonPressed()
        {
            _nextLevelPanel.gameObject.SetActive(false);
            _pauser.Resume();
        }
    }
}