using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents.Weapons.Bows
{
    internal class Bow : Weapon
    {
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private Transform _shootPoint;

        private ArrowsPool _pool;

        private void Start()
        {
            _pool = new ArrowsPool(_arrowPrefab, Damage, _layerMask);
        }

        public void Attack(IDamageable target)
        {
            Arrow arrow = _pool.GetArrow();

            arrow.transform.position = _shootPoint.position;
            arrow.Fly(target.Transform);
        }
    }
}
