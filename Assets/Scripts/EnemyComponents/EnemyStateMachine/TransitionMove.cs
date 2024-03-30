using System.Collections;
using Assets.Scripts.EnemyNamespace;
using UnityEngine;

namespace Assets.Scripts.UnitStateMachine
{
    internal class TransitionMove : Transition
    {
        [SerializeField] private EnemyNextTargetFinder _enemyNextTargetFinder;
        [SerializeField] private EnemyRayPoint _enemyRayPoint;
        [SerializeField] private EnemyVision _enemyVision;
        [SerializeField] private StateIdle _stateIdle;
        [SerializeField] private LayerMask _layerMask;

        private float _minDistanceToTargetForMelleeEnemy = 1.5f;
        private float _minDistanceToTargetForRangeEnemy = 8f;
        private float _minDistanteToTarget;

        protected override Coroutine CheckTransition { get; set; }

        protected override State NextState { get; set; }

        internal override State GetNextState()
        {
            return NextState;
        }

        internal override IEnumerator CheckTransitionIE()
        {
            bool isMinDistance = false;
            NextState = null;

            while (isMinDistance == false)
            {
                isMinDistance = CheckIsMinDistance();
                yield return null;
            }

            NextState = _stateIdle;
        }

        internal bool CheckIsMinDistance()
        {
            Debug.DrawRay(_enemyRayPoint.transform.position, _enemyRayPoint.transform.forward * _minDistanteToTarget, Color.red, 0.1f);

            return Physics.Raycast(_enemyNextTargetFinder.transform.position, _enemyNextTargetFinder.transform.forward, _minDistanteToTarget);
        }

        internal override void StartCheckTransition()
        {
            if (CheckTransition == null)
            {
                CheckTransition = StartCoroutine(CheckTransitionIE());
            }
        }

        internal override void StopCheckTransition()
        {
            CheckTransition = StartCoroutine(CheckTransitionIE());
            CheckTransition = null;
        }

        private void Awake()
        {
            if (TryGetComponent(out EnemyMelee enemyMelee))
            {
                _minDistanteToTarget = _minDistanceToTargetForMelleeEnemy;
            }
            else if (TryGetComponent(out EnemyRange enemyRange))
            {
                _minDistanteToTarget = _minDistanceToTargetForRangeEnemy;
            }
        }
    }
}