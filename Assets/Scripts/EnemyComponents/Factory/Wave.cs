using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    [System.Serializable]
    internal class Wave
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private int _meleeAmount;
        [SerializeField] private int _rangeAmount;
        [SerializeField] private int _spawnAmount;

        public int MeleeAmount => _meleeAmount;
        public int RangeAmount => _rangeAmount;
        public float SpawnDelay => _spawnDelay;
        public float SpawnAmount => _spawnAmount;
    }
}
