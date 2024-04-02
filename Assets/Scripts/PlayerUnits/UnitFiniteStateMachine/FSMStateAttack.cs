using Assets.Scripts.Constants;
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
            if (FSM.Target != null)
            {
                Debug.Log(FSM.Target);

                if (NeedChaseEnemy())
                {
                    FSM.SetState<FSMStateChaseEnemy>();
                }
                else
                {
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
            if (FSM.Target == null)
                return false;

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
                FSM.Target.TakeDamage(Data.Damage);
                Animator.SetTrigger(AnimatorHash.Attack);
                _timePast = 0;
            }
        }
    }
}
