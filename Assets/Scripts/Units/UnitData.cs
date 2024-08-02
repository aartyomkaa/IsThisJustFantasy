using UnityEngine;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.Units
{
    [CreateAssetMenu(fileName = "NewUnit", menuName = "Data/UnitData")]
    internal class UnitData : Data
    {
        public Unit Prefab;
    }
}
