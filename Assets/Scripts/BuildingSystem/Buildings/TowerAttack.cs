using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.PlayerComponents.Weapons;
using Assets.Scripts.PlayerComponents.Weapons.Bows;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem
{
    internal class TowerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private float _delayOfShoot;
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _targetlayerMask;

        private List<IDamageable> _targets = new();
        private ArrowsPool _poolOfArrows;
        private float _currentDelay;

        private void Awake()
        {
            _poolOfArrows = new ArrowsPool(_arrowPrefab, _damage, _targetlayerMask);
        }

        private void Update()
        {
            if (_targets.Count > 0)
            {
                TryToShoot();
                CleanTargets(_targets);
            }
        }

        private void TryToShoot()
        {
            foreach (IDamageable target in _targets)
            {
                if (target.Health > 0 && _currentDelay >= _delayOfShoot)
                {
                    Shoot(target);
                    _currentDelay = 0;
                }
            }

            _currentDelay += Time.deltaTime;
        }

        private void Shoot(IDamageable target)
        {
            Arrow arrow = _poolOfArrows.GetArrow();
            arrow.transform.position = _shootPoint.position;
            arrow.Fly(target.Transform);
        }

        private void CleanTargets(List<IDamageable> targets)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                if (targets[i] == null || targets[i].Transform.gameObject.activeSelf == false)
                { 
                    _targets.RemoveAt(i);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            int mask = 1 << other.gameObject.layer;

            if (other.gameObject.TryGetComponent(out IDamageable target) && mask == _targetlayerMask)
            {
                _targets.Add(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            int mask = 1 << other.gameObject.layer;

            if (other.gameObject.TryGetComponent(out IDamageable target) && mask == _targetlayerMask)
            {
                _targets.Remove(target);
            }
        }
    }
}