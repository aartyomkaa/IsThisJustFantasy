using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.GameLogic;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemyFactory: MonoBehaviour
    {
        [SerializeField] private EnemySpawnPoint[] _spawnPoints;
        [SerializeField] private Wave[] _waves;
        [SerializeField] private EnemyData _range;
        [SerializeField] private EnemyData _melee;
        [SerializeField] private MainBuilding _building;

        private EnemyPool _meleePool;
        private EnemyPool _rangePool;

        private Coroutine _waveCoroutine;
        private int _spawnPointIndex;

        public SceneSwitcher _sceneSwitcher;

        public event Action<float> WaveStarted;

        private void Start()
        {
            _meleePool = new EnemyPool(_melee, _building, transform);
            _rangePool = new EnemyPool(_range, _building, transform);

            _sceneSwitcher = new SceneSwitcher();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                StartWavee(0);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _sceneSwitcher.LoadScene("ArtjomScene");
            }
        }

        public void StartWavee(int waveNumber)
        {
            if (_waveCoroutine != null)
            {
                StopCoroutine(_waveCoroutine);
            }

            _waveCoroutine = StartCoroutine(SpawnWave(_waves[waveNumber]));
        }

        private void SpawnMelee(Vector3 position)
        {
            Enemy enemy = _meleePool.GetUnit();

            enemy.transform.position = position;
        }

        private void SpawnRange(Vector3 position)
        {
            Enemy enemy = _rangePool.GetUnit();

            enemy.transform.position = position;
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            float waitSeconds = wave.SpawnDelay;
            var waitForSec = new WaitForSeconds(waitSeconds);

            for (int i = 0; i < wave.MeleeAmount; i++)
            {
                SpawnMelee(_spawnPoints[_spawnPointIndex].transform.position);

                _spawnPointIndex = (_spawnPointIndex + 1) % _spawnPoints.Length;

                yield return waitForSec;
            }
            
            for (int i = 0; i < wave.RangeAmount; i++)
            {
                SpawnRange(_spawnPoints[_spawnPointIndex].transform.position);

                _spawnPointIndex = (_spawnPointIndex + 1) % _spawnPoints.Length;

                yield return waitForSec;
            }
        }
    }
}