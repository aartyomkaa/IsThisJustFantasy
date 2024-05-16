using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class ScorePanel : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _textScore;

        public event Action BackButtonClicked;
        public event Action ContinueButtonClicked;


        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }


        public void SetTextScore(Score playerScore)
        {
            _textScore.text = playerScore.TotalScore.ToString();
        }

        private void OnBackButtonClicked()
        {
            BackButtonClicked?.Invoke();
        }

        private void OnContinueButtonClicked()
        {
            ContinueButtonClicked?.Invoke();
        }



    }  
}