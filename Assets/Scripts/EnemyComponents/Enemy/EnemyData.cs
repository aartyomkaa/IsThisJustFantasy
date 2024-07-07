using UnityEngine;
using Assets.Scripts.GameLogic;
using Assets.Scripts.PlayerComponents.Weapons;

namespace Assets.Scripts.EnemyComponents
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Data/EnemyData")]
    internal class EnemyData : Data
    {
        public Enemy Prefab;
        public Weapon Weapon;
    }
}
