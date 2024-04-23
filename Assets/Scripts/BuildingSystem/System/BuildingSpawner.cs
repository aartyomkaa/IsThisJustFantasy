
using UnityEngine;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Props.Chest;
using Unity.VisualScripting;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuildingSpawner
    {
        private BuildingsPool _buildingsPool;
        public Building CurrentBuilding { get; private set; }   

        public BuildingSpawner( Tower tower, Barracks barracks, ResoorceBuilding resoorceBuilding)
        {
            _buildingsPool = new BuildingsPool(tower, barracks, resoorceBuilding);
        }

        public void Spawn(int SpawnPointIndex, Transform SpawnPointTransform, ChestSpawnerPointsContainer chestSpawnPoints)
        {
            CurrentBuilding = _buildingsPool.GetBuilding(SpawnPointIndex, chestSpawnPoints);
            CurrentBuilding.Transform.parent = SpawnPointTransform;  
        }
    }
}