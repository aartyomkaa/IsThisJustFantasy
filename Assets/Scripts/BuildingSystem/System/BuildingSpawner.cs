using UnityEngine;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Props.Chest;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuildingSpawner
    {
        private BuildingsPool _buildingsPool;
    
        public BuildingSpawner(Tower tower, Barracks barracks, ResoorceBuilding resoorceBuilding)
        {
            _buildingsPool = new BuildingsPool(tower, barracks, resoorceBuilding);
        }

        public Building CurrentBuilding { get; private set; }

        public void Spawn(int spawnPointIndex, Transform spawnPointTransform, ChestSpawnerPointsContainer chestSpawnPoints)
        {
            CurrentBuilding = _buildingsPool.GetBuilding(spawnPointIndex, chestSpawnPoints);
            CurrentBuilding.Transform.parent = spawnPointTransform;
        }
    }
}