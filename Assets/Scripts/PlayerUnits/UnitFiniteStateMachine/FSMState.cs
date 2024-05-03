using Assets.Scripts.EnemyComponents;
using Assets.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal abstract class FSMState
    {
        protected FiniteStateMachine FSM;
        protected IFSMControllable Unit;
        protected Data Data;
        protected NavMeshAgent UnitNavMesh;
        protected Animator Animator;
        protected UnitSFX EnemySFX;

        public FSMState(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data, UnitSFX unitSFX)
        {
            FSM = fsm;
            Unit = unit;
            UnitNavMesh = navMesh;
            Animator = animator;
            Data = data;
            EnemySFX = unitSFX;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}
