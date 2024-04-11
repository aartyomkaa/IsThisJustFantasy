using UnityEngine;

namespace Assets.Scripts.GameLogic.Interfaces
{
    internal interface IDamageable
    {
        public Transform Transform { get; }

        public void TakeDamage(float damage);
    }
}