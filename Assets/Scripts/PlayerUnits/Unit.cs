using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.PlayerUnits.UnitFiniteStateMachine;

namespace Assets.Scripts.PlayerUnits
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    internal class Unit : Selectable, IDamageable, IFSMControllable, IHealthDisplayable
    {
        [SerializeField] private AudioSource _audioSource;

        private UnitData _data;
        private float _health;

        private FiniteStateMachine _fsm;
        private Animator _animator;
        private NavMeshAgent _agent;
        private UnitSFX _unitSFX;
        private Coroutine _deathCoroutine;
        private float _deathDuration = 5f;

        public Transform Transform => transform;

        public float Health => _health;

        public event Action<float> HealthValueChanged;

        private void Start()
        {
            if (_health <= 0)
                return;

            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _unitSFX = GetComponentInChildren<UnitSFX>();

            _fsm = new FiniteStateMachine(_animator, _agent, this, _data, _unitSFX);

            _fsm.SetState<FSMStateIdle>();
        }

        public void Update() 
        {
            _fsm.Update();
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;

            HealthValueChanged?.Invoke(_health);

            if (_health <= 0)
                Die();
        }

        public void Init(UnitData data)
        {
            _data = data;
            _health = data.Health;
            HealthValueChanged?.Invoke(_health);
        }

        public void Move(Vector3 position)
        {
            _fsm.SetMovePosition(position);
            _fsm.SetState<FSMStateMove>();
        }

        public void Attack(IDamageable target)
        {
            _audioSource.Play();
            target.TakeDamage(_data.Damage);
        }

        private void Die()
        {
            if (_deathCoroutine != null)
            {
                StopCoroutine(_deathCoroutine);
            }

            _deathCoroutine = StartCoroutine(Death(_deathDuration));
        }

        private IEnumerator Death(float time)
        {
            _unitSFX.PlayDeathSound();
            _agent.ResetPath();
            _animator.SetTrigger(AnimatorHash.Death);

            yield return new WaitForSeconds(time);

            _health = _data.Health;
            gameObject.SetActive(false);
        }
    }
}