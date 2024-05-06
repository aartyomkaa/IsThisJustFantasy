using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal class FSMStateChaseEnemy : FSMState
    {
        public FSMStateChaseEnemy(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data, UnitSFX enemySFX)
            : base(fsm, unit, navMesh, animator, data, enemySFX)
        {
        }

        public override void Enter()
        {
            UnitSFX.PlayWalkSound();
            UnitNavMesh.SetDestination(FSM.Target.Transform.position);
            Animator.SetBool(AnimatorHash.Moving, true);
        }

        public override void Exit()
        {
            UnitNavMesh.ResetPath();
            Animator.SetBool(AnimatorHash.Moving, false);
        }

        public override void Update()
        {
            if (UnitNavMesh.remainingDistance > Data.AggroRange)
                FSM.SetState<FSMStateIdle>();

            if (UnitNavMesh.remainingDistance <= Data.AttackRange)
            {
                FSM.SetState<FSMStateAttack>();
            }
        }
    }
}
