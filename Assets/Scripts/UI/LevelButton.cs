using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _level;

          
        public event Action<string> Clicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Clicked?.Invoke(_level);
        }

        public void Active()
        {
            _button.gameObject.SetActive(true);
        }

        public void Deactive()   // ���� ����� �� �����
        {
            _button.gameObject.SetActive(false);
        }
    }
}