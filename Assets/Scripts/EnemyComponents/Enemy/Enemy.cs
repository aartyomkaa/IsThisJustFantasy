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
        private float _health;

        private EnemyData _data;
        private UnitSFX _unitSFX;
        private FiniteStateMachine _fsm;
        private Animator _animator;
        private NavMeshAgent _agent;

        private Coroutine _deathCoroutine;
        private float _deathDuration = 5f;

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

            _fsm = new FiniteStateMachine(_animator, _agent, this, _data, _unitSFX);

            _fsm.SetState<FSMStateIdle>();
        }

        private void Update()
        {
            if (_health <= 0)
                return;

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

        public virtual void Attack(IDamageable target)
        {
            target.TakeDamage(_data.Damage);
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
            _animator.SetTrigger(AnimatorHash.Death);

            yield return new WaitForSeconds(time);

            _health = _data.Health;
            gameObject.SetActive(false);
        }
    }
}