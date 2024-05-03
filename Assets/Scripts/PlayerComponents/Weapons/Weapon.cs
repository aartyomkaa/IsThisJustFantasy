using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents.Weapons
{
    internal abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected LayerMask EnemyLayerMask;
        [SerializeField] protected AudioSource AudioSource;

        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeed;

        protected Coroutine AttackCoroutine;

        public bool CanAttack { protected set; get; }

        public float Damage => _damage;

        public float AttackSpeed => _attackSpeed;

        private void Awake()
        {
            CanAttack = true;
        }

        public virtual void Attack()
        {
            if (CanAttack)
                AttackCoroutine = StartCoroutine(AttackDelay(_attackSpeed));
        }

        public void LevelUp(float damage, float attackSpeed)
        {
            _damage = damage;
            _attackSpeed = attackSpeed;
        }

        private IEnumerator AttackDelay(float attackSpeed)
        {
            AudioSource.Play();
            CanAttack = false;

            yield return new WaitForSeconds(attackSpeed);

            CanAttack = true;
        }
    }
}