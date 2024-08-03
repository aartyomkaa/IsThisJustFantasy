using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal interface IFSMControllable
    {
        public Transform Transform { get; }

        void Attack(IDamageable target);
    }
}