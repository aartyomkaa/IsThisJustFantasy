using Assets.Scripts.Constants;
using Assets.Scripts.EnemyComponents;
using Assets.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal class FSMStateAttack : FSMState
    {
        private float _distance;
        private float _timePast;

        public FSMStateAttack(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data)
            : base(fsm, unit, navMesh, animator, data)
        {
        }

        public override void Update()
        {
            if (FSM.Target != null && FSM.Target.Transform.gameObject.activeSelf)
            {
                if (NeedChaseEnemy())
                {
                    FSM.SetState<FSMStateChaseEnemy>();
                }
                else
                {
                    RotateTowards(FSM.Target.Transform.position);
                    Attack();
                }
            }
            else
            {
                FSM.SetState<FSMStateIdle>();
            }
        }

        private bool NeedChaseEnemy()
        {
            _distance = Vector3.Distance(Unit.Transform.position, FSM.Target.Transform.position);

            if (_distance > Data.AttackRange)
                return true;

            return false;
        }

        private void Attack()
        {
            _timePast += Time.deltaTime;

            if (_timePast >= Data.AttackSpeed)
            {
                if (Unit.GetType() == typeof(EnemyRange))
                    Unit.Attack(FSM.Target);
                else
                    FSM.Target.TakeDamage(Data.Damage);

                Animator.SetTrigger(AnimatorHash.Attack);

                _timePast = 0;
            }
        }

        private void RotateTowards(Vector3 targetPosition)
        {
            Vector3 directionToTarget = targetPosition - Unit.Transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

            Unit.Transform.rotation = Quaternion.Slerp(Unit.Transform.rotation, targetRotation, Time.fixedDeltaTime);
        }
    }
}
