using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerUnits.UnitFiniteStateMachine;
using Assets.Scripts.BuildingSystem.Buildings;

namespace Assets.Scripts.EnemyComponents
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    internal class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private bool _isChestGuard;

        private Animator _animator;
        private NavMeshAgent _agent;
        private NavMeshPath _path;
        private MainBuilding _target;
        private FiniteStateMachine _fsm;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _fsm = GetComponent<Enemy>().FSM;

            _path = new NavMeshPath();
        }

        private void Update()
        {
            if (_isChestGuard)
                return;

            if (_agent.hasPath == false && _fsm.Target == null)
            {
                SetDestination(_target.transform.position);
            }
        }

        public void InitTarget(MainBuilding target)
        {
            _target = target;
        }

        private void SetDestination(Vector3 position)
        {
            Debug.Log("1");
            _agent.CalculatePath(position, _path);
            _agent.SetPath(_path);
            _animator.SetBool(AnimatorHash.Moving, true);
        }
    }
}