using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Assets.Scripts.UI.Tutorial
{
    internal class Tutorial : MonoBehaviour
    {
        [SerializeField] private TutorialArrow[] _arrows;
        [SerializeField] private TutorialPanel[] _panels;

        private int _tutorialIndex = 0;
        private float _nextTutorialDelay = 15f;
        private Coroutine _nextTutorial;

        private void OnEnable()
        {
            foreach (var arrow in _arrows)
            {
                arrow.gameObject.SetActive(false);
                arrow.Collected += StartTutorialCoroutine;
            }
        }

        private void Start()
        {
            ShowNextTutorial();
        }

        private void OnDisable()
        {
            foreach(var arrow in _arrows)
            {
                arrow.Collected -= StartTutorialCoroutine;
            }
        }

        private void StartTutorialCoroutine()
        {
            _nextTutorial = StartCoroutine(NextTutorialDelay());
        }

        private IEnumerator NextTutorialDelay()
        {
            yield return new WaitForSeconds(_nextTutorialDelay);

            ShowNextTutorial();
        }

        private void ShowNextTutorial()
        {
            if (_tutorialIndex < _arrows.Length)
            {
                _arrows[_tutorialIndex].gameObject.SetActive(true);

                if (_panels[_tutorialIndex] != null)
                    _panels[_tutorialIndex].Open();

                _tutorialIndex++;
            }
        }
    }
}
