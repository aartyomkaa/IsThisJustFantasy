using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.PlayerComponents
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerAnimator))]
    internal class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;

        private NavMeshAgent _navMeshAgent;
        private PlayerAnimator _animator;

        private bool _isAttacking;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<PlayerAnimator>();
        }

        public void Move(Vector2 direction)
        {
            Vector3 movementDirection = new Vector3(direction.x, 0, direction.y);
            Vector3 movePosition = transform.position + movementDirection;

            _navMeshAgent.speed = _isAttacking ? _data.AttackMoveSpeed : _data.Speed;
            _navMeshAgent.SetDestination(movePosition);

            _animator.SetAnimatorSpeed(movementDirection, _data.Speed);
        }

        public void StopMove()
        {
            _navMeshAgent.updateRotation = false;
            _isAttacking = true;
        }

        public void StartMove()
        {
            _isAttacking = false;
            _navMeshAgent.updateRotation = true;
        }

        public void RotateTowards(Transform target, Vector3 offset)
        {
            Vector3 directionToTarget = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            offset = new Vector3(transform.rotation.x, offset.y, transform.rotation.z);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * Quaternion.Euler(offset), Time.fixedDeltaTime);
        }
    }
}