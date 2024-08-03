using UnityEngine;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.Units
{
    [CreateAssetMenu(fileName = "NewUnit", menuName = "Data/UnitData")]
    internal class UnitData : Data
    {
        [SerializeField] private Unit _prefab;

        public Unit Prefab => _prefab;
    }
}