using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal class FiniteStateMachine
    {
        private IFSMControllable _a;
        private FSMState _currentState;
        private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState>();

        public Vector3 MovePosition { get; private set; }
        public IDamageable Target { get; private set; }

        public FiniteStateMachine(Animator animator, NavMeshAgent agent, IFSMControllable unit, Data data, UnitSFX unitSFX)
        {
            AddState(new FSMStateIdle(this, unit, agent, animator, data, unitSFX));
            AddState(new FSMStateMove(this, unit, agent, animator, data, unitSFX));
            AddState(new FSMStateChaseEnemy(this, unit, agent, animator, data, unitSFX));
            AddState(new FSMStateAttack(this, unit, agent, animator, data, unitSFX));

            _a = unit;
        }

        public void AddState(FSMState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : FSMState
        {
            var type = typeof(T);

            if (_currentState?.GetType() == typeof(FSMStateMove) || _currentState?.GetType() != type)
            {
                if (_states.TryGetValue(type, out var newState))
                {
                    _currentState?.Exit();

                    _currentState = newState;

                    _currentState.Enter();
                }
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void SetMovePosition(Vector3 position)
        {
            MovePosition = position;
        }

        public void SetTarget(IDamageable target)
        {
            Target = target;
        }
    }
}