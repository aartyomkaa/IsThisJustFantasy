using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.PlayerUnits
{
    internal class UnitsFactory : MonoBehaviour
    {
        [SerializeField] private UnitData _unitData;
        [SerializeField] private Transform _spotOfRespawnUnits;
        [SerializeField] private SelectedUnitsHandler _handler;

        private UnitsPool _pool;

        private void Start() 
        {
            _pool = new UnitsPool(_unitData, transform.localPosition);
            _handler.Init(_pool.MeleePool);
        }

        public void Spawn()  
        {
            Unit unit = _pool.GetUnit();
            NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
            agent.Warp(_spotOfRespawnUnits.transform.position); 
        }
    }
}
