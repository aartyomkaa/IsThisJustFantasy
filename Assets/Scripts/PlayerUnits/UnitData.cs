using UnityEngine;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.PlayerUnits
{
    [CreateAssetMenu(fileName = "NewUnit", menuName = "Data/UnitData")]
    internal class UnitData : Data
    {
        public Unit Prefab;
    }
}
