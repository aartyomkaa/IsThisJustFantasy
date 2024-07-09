using UnityEngine;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.Weapons.Bows;

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