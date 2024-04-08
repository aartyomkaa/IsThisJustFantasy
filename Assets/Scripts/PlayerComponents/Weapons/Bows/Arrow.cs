using Assets.Scripts.GameLogic.Damageable;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents.Weapons
{
    internal class Arrow : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private float _speed;

        private LayerMask _layerMask;

        private float _damage;

        private Coroutine _flying;

        private void OnTriggerEnter(Collider other)
        {   
            int mask = 1 << other.gameObject.layer;
           
            if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable target) && mask == _layerMask)
            {
                target.TakeDamage(_damage);
                ParticleSystem hitEffect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
                Destroy(hitEffect.gameObject, 1f);

                if (_flying != null)
                {
                    StopCoroutine(_flying);
                }

                gameObject.SetActive(false);
            }
        }

        public void Fly(Vector3 targetPosition)
        {
            if (_flying != null)
            {
                StopCoroutine(_flying);
            }

            _flying = StartCoroutine(Flying(targetPosition));
        }

        public void Init(float damage, LayerMask targetMask)
        {
            _damage = damage;
            _layerMask = targetMask;
        }

        private IEnumerator Flying(Vector3 targetPosition)
        {
            Vector3 relativePosition = targetPosition - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePosition, Vector3.up);

            while (targetPosition != null && Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}