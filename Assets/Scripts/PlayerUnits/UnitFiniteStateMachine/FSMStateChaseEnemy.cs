using Assets.Scripts.Constants;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.AI;

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
