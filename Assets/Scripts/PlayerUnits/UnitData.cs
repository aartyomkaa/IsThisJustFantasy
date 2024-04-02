using Assets.Scripts.GameLogic;
using UnityEngine;

namespace Assets.Scripts.PlayerUnits
{
    [CreateAssetMenu(fileName = "NewUnit", menuName = "Data/UnitData")]
    internal class UnitData : Data
    {
        public Unit Prefab;
    }
}
