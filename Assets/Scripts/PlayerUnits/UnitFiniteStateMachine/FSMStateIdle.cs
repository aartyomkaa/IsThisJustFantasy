using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Damageable;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal class FSMStateIdle : FSMState
    {
        private ClosestTargetFinder _targetFinder;
        private IDamageable _target;

        public FSMStateIdle(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data) 
            : base(fsm, unit, navMesh, animator, data)
        {
            _targetFinder = new ClosestTargetFinder(data.AggroRange, data.EnemyLayerMask);
        }

        public override void Enter()
        {
            FSM.SetEnemy(null);
        }

        public override void Update() 
        { 
            if (_targetFinder.TryFindTarget(Unit.Transform.position, out _target))
            {
                FSM.SetEnemy(_target);
                FSM.SetState<FSMStateChaseEnemy>();
            }
        }
    }
}