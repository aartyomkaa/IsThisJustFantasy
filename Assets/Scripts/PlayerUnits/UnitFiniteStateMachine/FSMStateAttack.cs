﻿using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using Assets.Scripts.Audio;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal class FSMStateAttack : FSMState
    {
        private float _distance;
        private float _timePast;

        public FSMStateAttack(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data, UnitSFX unitSFX)
            : base(fsm, unit, navMesh, animator, data, unitSFX)
        {
        }

        public override void Update()
        {
            if (FSM.Target != null && FSM.Target.Transform.gameObject.activeSelf && FSM.Target.Health > 0)
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
                Unit.Attack(FSM.Target);
                UnitSFX.PlayAttackSound();

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
