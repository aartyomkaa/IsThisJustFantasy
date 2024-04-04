using Assets.Scripts.GameLogic.Damageable;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal interface IFSMControllable
    {
        public Transform Transform { get; }

        public abstract void Attack(IDamageable target);
    }
}
