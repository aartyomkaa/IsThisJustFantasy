using Assets.Scripts.BuildingSystem.Buildings;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemyPool
    {
        private Enemy[] _enemyPool;

        private int _capacity = 20;

        public EnemyPool(EnemyData data, MainBuilding building)
        {
            _enemyPool = CreateUnitsPool(data, building);
        }

        public Enemy GetEnemy()
        {
            foreach (var melee in _enemyPool)
            {
                if (melee.gameObject.activeSelf == false)
                {
                    melee.gameObject.SetActive(true);

                    return melee;
                }
            }

            throw new System.Exception("Not enough units in The pool!!!");
        }

        private Enemy[] CreateUnitsPool(EnemyData data, MainBuilding building)
        {
            Enemy[] pool = new Enemy[_capacity];

            for (int i = 0; i < _capacity; i++)
            {
                Enemy unit = GameObject.Instantiate(data.Prefab);
                unit.Init(data, building);
                unit.gameObject.SetActive(false);
                pool[i] = unit;
            }

            return pool;
        }
    }
}
