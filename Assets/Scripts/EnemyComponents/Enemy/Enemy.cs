using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Audio;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.PlayerUnits.UnitFiniteStateMachine;

namespace Assets.Scripts.EnemyComponents
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    internal abstract class Enemy : MonoBehaviour, IDamageable, IFSMControllable, IHealthDisplayable
    {
        [SerializeField] private bool _isChestGuard;
        [SerializeField] private EnemyData _data;

        private float _health;

        private UnitSFX _unitSFX;
        private FiniteStateMachine _fsm;
        private Animator _animator;
        private NavMeshAgent _agent;

        private Coroutine _deathCoroutine;
        private float _deathDuration = 6f;

        private MainBuilding _building;

        public Transform Transform => transform;

        public float Health => _health;

        public event Action<Enemy> Died;
        public event Action<float> HealthValueChanged;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _unitSFX = GetComponentInChildren<UnitSFX>();

            if (_isChestGuard)
            {
                if (_data != null)
                    _health = _data.Health;
                else
                    throw new Exception("Set the data!");
            }

            _fsm = new FiniteStateMachine(_animator, _agent, this, _data, _unitSFX);

            _fsm.SetState<FSMStateIdle>();
        }

        private void Update()
        {
            if (_health <= 0)
                return;

            _fsm.Update();

            if (_isChestGuard == false)
            {
                SetDestination(_building.transform.position);
            }
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            HealthValueChanged?.Invoke(_health);

            if (_health <= 0)
            {
                Die();
            }
        }

        public void Init(EnemyData data, MainBuilding target)
        {
            _data = data;
            _health = data.Health;
            _building = target;
        }

        public virtual void Attack(IDamageable target)
        {
            target.TakeDamage(_data.Damage);
        }

        private void SetDestination(Vector3 position)
        {
            if (_agent.destination != position && _fsm.Target == null)
            {
                _agent.SetDestination(position);
                _animator.SetBool(AnimatorHash.Moving, true);
            }
        }

        private void Die()
        {
            if (_deathCoroutine != null)
            {
                StopCoroutine(_deathCoroutine );
            }

            _deathCoroutine = StartCoroutine(Death(_deathDuration));
        }

        private IEnumerator Death(float time)
        {
            Died?.Invoke(this);
            _unitSFX.PlayDeathSound();
            _fsm.SetTarget(null);
            _agent.ResetPath();
            _animator.SetTrigger(AnimatorHash.Death);

            yield return new WaitForSeconds(time);

            _health = _data.Health;
            HealthValueChanged?.Invoke(_health);
            gameObject.SetActive(false);
        }
    }
}