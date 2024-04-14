using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal abstract class Data : ScriptableObject
    {
        public string Name;
        public LayerMask EnemyLayerMask;
        public float Health;
        public float Speed;
        public float Damage;
        public float AttackSpeed;
        public float AttackRange;
        public float AggroRange;
    }
}
