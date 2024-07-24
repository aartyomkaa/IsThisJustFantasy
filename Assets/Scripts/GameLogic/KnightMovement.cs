using UnityEngine;
using Assets.Scripts.PlayerUnits;
using Assets.Scripts.PlayerUnits.UnitFiniteStateMachine;

namespace Assets.Scripts.GameLogic
{
    [RequireComponent(typeof(Knight))]
    internal class KnightMovement : MonoBehaviour
    {
        private FiniteStateMachine _fsm;

        private void Start()
        {
            _fsm = GetComponent<Knight>().FSM;
        }

        public void Move(Vector3 position)
        {
            _fsm.SetMovePosition(position);
            _fsm.SetState<FSMStateMove>();
        }
    }
}