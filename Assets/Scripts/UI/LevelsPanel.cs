using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class LevelsPanel : MonoBehaviour
    {
        [SerializeField] private List<LevelButton> _levelButtons;
        [SerializeField] private SceneLoader _sceneLoader;


        private int _lastAvailableLevel = 1;


        private void Start()
        {
            SetLastAvailableLevel();
            ActiveAvailableLevels();
        }


        private void OnEnable()
        {
            SignToButtons();
        }

        private void OnDisable()
        {
            UnSignToButtons();
        }


        private void ActiveAvailableLevels()
        {
            for (int i = 0; i < _lastAvailableLevel; i++)
            {
                _levelButtons[i].Active();
            }
        }

        private void SetLastAvailableLevel()
        {
            if (PlayerPrefs.GetInt(SceneNames.LastAvailableLevel) > _lastAvailableLevel)
            {
                _lastAvailableLevel = PlayerPrefs.GetInt(SceneNames.LastAvailableLevel);

                Debug.Log("��������� ��������� ������� ��� ����� -  " + _lastAvailableLevel);
            }
        }

        private void SignToButtons()
        {
            for (int i = 0; i < _lastAvailableLevel; i++)
            {
                _levelButtons[i].Clicked += LoadLevel;
            }
        }

        private void UnSignToButtons()
        {
            for (int i = 0; i < _lastAvailableLevel; i++)
            {
                _levelButtons[i].Clicked -= LoadLevel;
            }
        }

        private void LoadLevel(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName);
        }
    }
}