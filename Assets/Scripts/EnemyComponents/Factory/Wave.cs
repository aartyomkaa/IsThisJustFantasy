using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class Wave
    {
        private float _delayBetweenWaves;
        private float _maxRangeOfSpawn;
        private float _enemyCountInWave;

        private int _meleeEnemeyAmount;
        private int _rangeEnemeyAmount;

        public Wave(int melee, int range)
        {
            _meleeEnemeyAmount = melee;
            _rangeEnemeyAmount = range;
        }
    }
}
