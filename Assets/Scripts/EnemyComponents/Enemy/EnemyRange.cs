using Assets.Scripts.GameLogic.Damageable;
using Assets.Scripts.PlayerComponents.Weapons.Bows;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemyRange : Enemy
    {
        [SerializeField] private Bow _bow;

        public override void Attack(IDamageable target)
        {
            _bow.Attack(target);
        }
    }
}