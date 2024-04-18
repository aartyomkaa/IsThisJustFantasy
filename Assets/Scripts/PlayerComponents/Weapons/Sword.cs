using Assets.Scripts.GameLogic.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents.Weapons
{
    [RequireComponent(typeof(Collider))]
    internal class Sword : Weapon
    {
        private Coroutine _attackCoroutine;
        private Collider _swordCollider;
        private List<Transform> _targets = new();

        private void Start()
        {
            _swordCollider = GetComponent<Collider>();

            _swordCollider.enabled = false;
        }

        public override void Attack()
        {
            _swordCollider.enabled = true;

            
        }

        private void OnTriggerEnter(Collider other)
        {
            int mask = 1 << other.gameObject.layer;

            if (other.gameObject.TryGetComponent(out IDamageable target) && mask == EnemyLayerMask)
            {
                _targets.Add(target.Transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            int mask = 1 << other.gameObject.layer;

            if (other.gameObject.TryGetComponent(out IDamageable target) && mask == EnemyLayerMask)
            {
                _targets.Remove(target.Transform);
            }
        }

        private IEnumerator AttackDelay()
        {
            base.Attack();

            yield return new WaitForSeconds(0.1f);

            _swordCollider.enabled = false;
        }
    }
}
