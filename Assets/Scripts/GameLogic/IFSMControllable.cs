using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal interface IFSMControllable
    {
        public Transform Transform { get; }

        public abstract void Attack(IDamageable target);
    }
}
