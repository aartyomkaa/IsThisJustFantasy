using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.GameLogic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemyFactory: MonoBehaviour
    {
        [SerializeField] private EnemySpawnPoint[] _spawnPoints;
        [SerializeField] private Wave[] _waves;
        [SerializeField] private EnemyData _range;
        [SerializeField] private EnemyData _melee;
        [SerializeField] private MainBuilding _building;
        [SerializeField] private Button _start;
        [SerializeField] private SceneLoader _sceneSwitcher;

        private EnemyPool _meleePool;
        private EnemyPool _rangePool;
        private int _enemySpawned;

        private Coroutine _waveCoroutine;
        private int _spawnPointIndex;
        private int _waveIndex = 0;

        public event Action<int> WaveStarted;
        public event Action NextSpawned;

        private void OnEnable()
        {
            _start.onClick.AddListener(StartWave);
        }

        private void Start()
        {
            _meleePool = new EnemyPool(_melee, _building);
            _rangePool = new EnemyPool(_range, _building);
        }

        private void OnDisable()
        {
            _start.onClick.RemoveListener(StartWave);
        }

        public void StartWave()
        {
            if (_waves.Length > _waveIndex)
            {
                WaveStarted?.Invoke(_waves[_waveIndex].SpawnAmount);
                _enemySpawned = 0;

                if (_waveCoroutine != null)
                    StopCoroutine(_waveCoroutine);

                _waveCoroutine = StartCoroutine(SpawnWave(_waves[_waveIndex]));

                _waveIndex++;
            }
        }

        private void ChangeSpawnAmount(bool increase)
        {
            if (_waveIndex + 1 < _waves.Length)
                _waves[_waveIndex + 1].ChangeSpawnAmount(increase);
        }

        private void SpawnEnemy(EnemyPool pool, Vector3 position)
        {
            Enemy enemy = pool.GetEnemy();
            enemy.Died += OnEnemyDied;
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            agent.Warp(position);
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            _enemySpawned = (wave.MeleeAmount + wave.RangeAmount) * wave.SpawnAmount;

            for (int i = 0; i < wave.SpawnAmount; i++)
            {
                for (int j = 0; j < wave.MeleeAmount; j++)
                    SpawnEnemy(_meleePool, _spawnPoints[_spawnPointIndex].transform.position);

                for (int k = 0; k < wave.RangeAmount; k++)
                    SpawnEnemy(_rangePool, _spawnPoints[_spawnPointIndex].transform.position);

                _spawnPointIndex = (i + 1) % _spawnPoints.Length;
                NextSpawned?.Invoke();

                yield return wave.SpawnDelay;
            }
        }

        private void OnEnemyDied(Enemy enemy)
        {
            _enemySpawned--;
            enemy.Died -= OnEnemyDied;

            if (_enemySpawned == 0 && _waves.Length == _waveIndex)
                _sceneSwitcher.gameObject.SetActive(true);
        }
    }
}