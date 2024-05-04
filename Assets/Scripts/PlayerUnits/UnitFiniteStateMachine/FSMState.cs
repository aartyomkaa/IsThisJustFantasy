using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal abstract class FSMState
    {
        protected FiniteStateMachine FSM;
        protected IFSMControllable Unit;
        protected Data Data;
        protected NavMeshAgent UnitNavMesh;
        protected Animator Animator;
        protected UnitSFX UnitSFX;

        public FSMState(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data, UnitSFX unitSFX)
        {
            FSM = fsm;
            Unit = unit;
            UnitNavMesh = navMesh;
            Animator = animator;
            Data = data;
            UnitSFX = unitSFX;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}
