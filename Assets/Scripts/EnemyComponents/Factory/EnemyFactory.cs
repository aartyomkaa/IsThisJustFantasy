using Assets.Scripts.BuildingSystem.Buildings;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemyFactory: MonoBehaviour
    {
        [SerializeField] private List<EnemySpawnPoint> _spawnPoints;
        [SerializeField] private EnemyData _range;
        [SerializeField] private EnemyData _melee;
        [SerializeField] private MainBuilding _building;

        private EnemySpawnPoint _currentSpawnPoint;

        private EnemyPool _meleePool;
        private EnemyPool _rangePool;

        private void Start()
        {
            _meleePool = new EnemyPool(_melee, _building);
            _rangePool = new EnemyPool(_range, _building);

            foreach(EnemySpawnPoint spawnPoint in _spawnPoints)
            {
                Spawn(spawnPoint.transform.position);
                Spawn(spawnPoint.transform.position);
            }
        }

        public void Spawn(Vector3 position)
        {
            Enemy enemy = _meleePool.GetUnit();

            enemy.transform.position = position;
        }

        private void ChooseSpawnPoint()
        {
            foreach (EnemySpawnPoint spawnPoint in _spawnPoints)
            {

            }
        }

        private void CalculateEnemyPosition()
        {

        }

        private void EnableRandomEnemy()
        {

        }
    }
}