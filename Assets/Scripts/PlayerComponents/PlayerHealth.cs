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

        private PlayerSFX _playerSFX;
        private Coroutine _damageRecover;
        private WaitForSeconds _recoverTime;

        public Transform Transform => transform;
        public float Health => _value;

        public event Action<float> ValueChanged;

        public override void Init(PlayerData data, PlayerSFX sfx)
        {
            _playerSFX = sfx;
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
                _playerSFX.PlayTakeHitSound();
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
            _playerSFX.PlayHealSound();
        }

        private IEnumerator DamageRecover()
        {
            _canTakeDamage = false;

            yield return _recoverTime;

            _canTakeDamage = true;
        }
    }
}
