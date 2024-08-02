using UnityEngine;
using Assets.Scripts.GameLogic.Interfaces;

namespace Assets.Scripts.GameLogic
{
    internal interface IFSMControllable
    {
        public Transform Transform { get; }

        void Attack(IDamageable target);
    }
}
