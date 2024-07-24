using System;
using Assets.Scripts.PlayerUnits;

namespace Assets.Scripts.EnemyComponents
{
    internal abstract class Enemy : Unit
    {
        public event Action<Enemy> Died;

        public override void Die()
        {
            base.Die();

            Died?.Invoke(this);
        }
    }
}