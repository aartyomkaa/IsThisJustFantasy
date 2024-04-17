using UnityEngine;
using System.Collections;
using Assets.Scripts.GameLogic.Interfaces;
using System;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerData _playerData;

        private float _value;
        private bool _canTakeDamage = true;

        private Coroutine _damageRecover;
        private WaitForSeconds _recoverTime;

        public Transform Transform => transform;
        public float Value => _value;

        public event Action<float> ValueChanged;

        private void Awake()
        {
            _value = _playerData.Health;
            _recoverTime = new WaitForSeconds(_playerData.RecoverTime);
        }

        public void TakeDamage(float damage)
        {
            if (_canTakeDamage)
            {
                _value -= damage;
                ValueChanged?.Invoke(_value);
            }

            if (_value <= 0)
            {
                gameObject.SetActive(false);
            }
            else if (_canTakeDamage)
            {
                if (_damageRecover != null)
                {
                    StopCoroutine(_damageRecover);
                }

                _damageRecover = StartCoroutine(DamageRecover());
            }
        }

        public void Heal(float importHealValue)
        {
            _value += importHealValue;

            if (_value > _playerData.Health) 
            {
                _value = _playerData.Health;
            }
        }

        private IEnumerator DamageRecover()
        {
            _canTakeDamage = false;

            yield return _recoverTime;

            _canTakeDamage = true;
        }
    }
}
