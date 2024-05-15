using UnityEngine;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerComponents.Weapons;

namespace Assets.Scripts.AnimatorScripts
{
    internal class PlayerBowAttack : StateMachineBehaviour
    {
        [SerializeField] private Player _player;

        private PlayerBow _bow;
        private PlayerMovement _movement;
        private Vector3 _rotationOffset = new Vector3(0, 75, 0);

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_movement == null || _bow == null)
            {
                _movement = animator.GetComponent<PlayerMovement>();
                _bow = animator.GetComponentInChildren<PlayerBow>();
            }

            _movement.StopMove();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _movement.RotateTowards(_bow.Target, _rotationOffset);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _movement.StartMove();
        }
    }
}
