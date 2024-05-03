using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.PlayerUnits.UnitFiniteStateMachine;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.EnemyComponents
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    internal abstract class Enemy : MonoBehaviour, IDamageable, IFSMControllable, IHealthDisplayable
    {
        private float _health;

        private EnemyData _data;
        private UnitSFX _enemySFX;
        private FiniteStateMachine _fsm;
        private Animator _animator;
        private NavMeshAgent _agent;

        private MainBuilding _building;

        public Transform Transform => transform;

        public event Action<Enemy> Died;
        public event Action<float> HealthValueChanged;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();

            _fsm = new FiniteStateMachine(_animator, _agent, this, _data);

            _fsm.SetState<FSMStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();

            if (_fsm.Target == null && _agent.destination != _building.transform.position)
            {
                _agent.SetDestination(_building.transform.position);
                _animator.SetBool(AnimatorHash.Moving, true);
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

        private void Die()
        {
            Died?.Invoke(this);
            _health = _data.Health;
            _fsm.SetState<FSMStateIdle>();
            gameObject.SetActive(false);
        }

        public virtual void Attack(IDamageable target)
        {
            target.TakeDamage(_data.Damage);
        }
    }
}