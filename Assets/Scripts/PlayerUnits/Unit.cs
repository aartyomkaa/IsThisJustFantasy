using UnityEngine;
using Assets.Scripts.GameLogic.Damageable;
using Assets.Scripts.PlayerUnits.UnitFiniteStateMachine;
using UnityEngine.AI;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.PlayerUnits
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    internal class Unit : Selectable, IDamageable, IFSMControllable
    {
        private UnitData _unitData;
        private float _health;

        private FiniteStateMachine _fsm;
        private Animator _animator;
        private NavMeshAgent _agent;

        public Transform Transform => transform;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();

            _fsm = new FiniteStateMachine(_animator, _agent, this, _unitData);

            _fsm.SetState<FSMStateIdle>();
        }

        public void Update() 
        {
            _fsm.Update();
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
                Die();
        }

        public void Init(UnitData data)
        {
            _unitData = data;
            _health = data.Health;
        }

        public void Move(Vector3 position)
        {
            _fsm.SetMovePosition(position);
            _fsm.SetState<FSMStateMove>();
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}