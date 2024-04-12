using UnityEngine;
using System.Collections;
using Assets.Scripts.GameLogic.Interfaces;
using System;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerData _playerData;

        private float _value;
        private float _recoverTime;
        private bool _canTakeDamage;

        private Coroutine _damageRecover;

        public Transform Transform => transform;
        public float Value => _value;

        public event Action<float> ValueChanged;

        private void Awake()
        {
            _value = _playerData.Health;
            _recoverTime = _playerData.RecoverTime;
        }

        public void TakeDamage(float damage)
        {
            if (_canTakeDamage)
                _value -= damage;

            ValueChanged?.Invoke(_value);

            if (_value <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                if (_damageRecover != null)
                {
                    StopCoroutine(_damageRecover);
                }

                _damageRecover = StartCoroutine(DamageRecover(_recoverTime));
            }
        }

        private IEnumerator DamageRecover(float time)
        {
            _canTakeDamage = false;

            yield return new WaitForSeconds(time);

            _canTakeDamage = true;
        }
    }
}
