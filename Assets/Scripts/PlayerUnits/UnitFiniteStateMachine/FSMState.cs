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

        public FSMState(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data)
        {
            FSM = fsm;
            Unit = unit;
            UnitNavMesh = navMesh;
            Animator = animator;
            Data = data;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}
