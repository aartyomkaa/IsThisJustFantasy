using Assets.Scripts.GameLogic;
using Assets.Scripts.PlayerComponents.Weapons;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Data/EnemyData")]
    internal class EnemyData : Data
    {
        public Enemy Prefab;
        public Weapon Weapon;
    }
}
