using Assets.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class UnitHealthbar : MonoBehaviour
    {
        [SerializeField] private Slider _health;
        [SerializeField] private GameObject _unit;

        private IHealthDisplayable _displayable;
        private Vector3 _lookRotation;
        private Camera _camera;

        private void OnEnable()
        {
            if (_unit.TryGetComponent<IHealthDisplayable>(out IHealthDisplayable unit))
            {
                _displayable = unit;

                _displayable.HealthValueChanged += OnHealthChanged;
            }
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            _lookRotation = _camera.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(_lookRotation);
        }

        private void OnDisable()
        {
            _displayable.HealthValueChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float health)
        {
            _health.value = health;
        }
    }
}
