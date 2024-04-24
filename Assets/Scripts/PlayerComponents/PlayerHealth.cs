using UnityEngine;
using System.Collections;
using Assets.Scripts.GameLogic.Interfaces;
using System;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerHealth : PlayerComponent, IDamageable
    {
        private float _value;
        private float _maxHealth;
        private bool _canTakeDamage = true;

        private Coroutine _damageRecover;
        private WaitForSeconds _recoverTime;

        public Transform Transform => transform;
        public float Value => _value;

        public event Action<float> ValueChanged;

        public override void Init(PlayerData data)
        {
            _maxHealth = data.Health;
            _value = _maxHealth;
            _recoverTime = new WaitForSeconds(data.RecoverTime);
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

            _value = _value > _maxHealth ? _maxHealth : _value;        
        }

        private IEnumerator DamageRecover()
        {
            _canTakeDamage = false;

            yield return _recoverTime;

            _canTakeDamage = true;
        }
    }
}
