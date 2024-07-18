using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeed;

        private Coroutine AttackCoroutine;

        public bool CanAttack { protected set; get; }

        public float Damage => _damage;

        public float AttackSpeed => _attackSpeed;

        public LayerMask EnemyLayerMask => _enemyLayerMask;

        private void OnEnable()
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
            _damage += damage;
            _attackSpeed -= attackSpeed;
        }

        private IEnumerator AttackDelay(float attackSpeed)
        {
            _audioSource.Play();
            CanAttack = false;

            //кэш
            yield return new WaitForSeconds(attackSpeed);

            CanAttack = true;
        }
    }
}