using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents.Weapons
{
    internal class Sword : Weapon
    {
        private RaycastHit[] _hitColliders;
        private float _maxDistance = 1f;

        public override void Attack()
        {
            base.Attack();

            _hitColliders = Physics.BoxCastAll(transform.position, transform.localScale / 5500, transform.forward, Quaternion.identity, _maxDistance, EnemyLayerMask);

            if (_hitColliders.Length > 0)
            {
                foreach (var hit in _hitColliders)
                {
                    if (hit.transform.gameObject.TryGetComponent<IDamageable>(out IDamageable enemy))
                    {
                        enemy.TakeDamage(Damage);
                    }
                }
            }
        }
    }
}