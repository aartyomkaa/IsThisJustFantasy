using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal class FSMStateMove : FSMState
    {
        private Vector3 _roundedUnitPos;
        private Vector3 _roundedTargetPos;
        private NavMeshPath _path;

        public FSMStateMove(FiniteStateMachine fsm, IFSMControllable unit, NavMeshAgent navMesh, Animator animator, Data data, UnitSFX unitSFX)
            : base(fsm, unit, navMesh, animator, data, unitSFX)
        {
            _path = new NavMeshPath();
        }

        public override void Enter()
        {
            UnitSFX.PlayWalkSound();
            UnitNavMesh.speed = Data.Speed;
            UnitNavMesh.CalculatePath(FSM.MovePosition, _path);
            UnitNavMesh.SetPath(_path);
            Animator.SetBool(AnimatorHash.Moving, true);
        }

        public override void Exit()
        {
            Animator.SetBool(AnimatorHash.Moving, false);
        }

        public override void Update()
        {
            if (HasArriveDestination(Unit.Transform.position, UnitNavMesh.pathEndPosition))
            {
                FSM.SetState<FSMStateIdle>();
            }
        }

        private bool HasArriveDestination(Vector3 position, Vector3 targetPosition)
        {
            _roundedUnitPos = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));
            _roundedTargetPos = new Vector3(Mathf.RoundToInt(targetPosition.x), Mathf.RoundToInt(targetPosition.y), Mathf.RoundToInt(targetPosition.z));

            return _roundedUnitPos == _roundedTargetPos;
        }
    }
}
