using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.EnemyComponents
{
    internal class EnemyFactory: MonoBehaviour
    {
        [SerializeField] private EnemySpawnPoint[] _spawnPoints;
        [SerializeField] private Wave[] _waves;
        [SerializeField] private EnemyData _range;
        [SerializeField] private EnemyData _melee;
        [SerializeField] private MainBuilding _building;
        //[SerializeField] private Button _start;
        [SerializeField] private SceneLoader _sceneSwitcher;

        private EnemyPool _meleePool;
        private EnemyPool _rangePool;

        private Coroutine _waveCoroutine;
        private int _spawnPointIndex;
        private int _wave = 0;

        public event Action<float> WaveStarted;

        private void OnEnable()
        {
            //_start.onClick.AddListener(StartWave);
        }

        private void Start()
        {
            _meleePool = new EnemyPool(_melee, _building);
            _rangePool = new EnemyPool(_range, _building);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _sceneSwitcher.LoadScene(SceneNames.Level2);
            }  
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartWave();
            }
        }

        private void OnDisable()
        {
            //_start.onClick.RemoveListener(StartWave);
        }

        public void StartWave()
        {
            if (_waveCoroutine != null)
            {
                StopCoroutine(_waveCoroutine);
            }

            _waveCoroutine = StartCoroutine(SpawnWave(_waves[_wave]));

            _wave++;
        }

        private void SpawnMelee(Vector3 position)
        {
            Enemy enemy = _meleePool.GetUnit();

            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            agent.Warp(position);
        }

        private void SpawnRange(Vector3 position)
        {
            Enemy enemy = _rangePool.GetUnit();

            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            agent.Warp(position);
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            var waitForSec = new WaitForSeconds(wave.SpawnDelay);

            for (int i = 0; i < wave.SpawnAmount; i++)
            {
                for (int j = 0; j < wave.MeleeAmount; j++)
                {
                    SpawnMelee(_spawnPoints[_spawnPointIndex].transform.position);
                }

                for (int k = 0 ; k < wave.RangeAmount; k++)
                {
                    SpawnRange(_spawnPoints[_spawnPointIndex].transform.position);
                }

                _spawnPointIndex = i % _spawnPoints.Length;

                yield return waitForSec;
            }
        }
    }
}