using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal class FSMStateMove : FSMState
    {
        public FSMStateMove(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data)
            : base(fsm, unit, navMesh, animator, data)
        {
        }

        public override void Enter()
        {
            UnitNavMesh.speed = Data.Speed;
            UnitNavMesh.SetDestination(FSM.MovePosition);
            Animator.SetBool(AnimatorHash.Moving, true);
        }

        public override void Exit()
        {
            Animator.SetBool(AnimatorHash.Moving, false);
        }

        public override void Update()
        {
            if (UnitNavMesh.pathEndPosition == Unit.Transform.position)
            {
                FSM.SetState<FSMStateIdle>();
            }
        }
    }
}
