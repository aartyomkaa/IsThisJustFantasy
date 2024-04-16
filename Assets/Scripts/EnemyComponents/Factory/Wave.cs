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

        public float SpawnDelay => _spawnDelay;
        public int MeleeAmount => _meleeAmount;
        public int RangeAmount => _rangeAmount;
        public int SpawnAmount => _spawnAmount;

        public void ChangeSpawnAmount(bool increase)
        {
            _spawnAmount += increase ? 1 : -1;
        }

      
    }
}
