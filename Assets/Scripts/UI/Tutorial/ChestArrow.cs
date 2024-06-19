using UnityEngine;

namespace Assets.Scripts.UI.Tutorial
{
    internal class ChestArrow : MonoBehaviour
    {
        [SerializeField] private Transform _chest;
        
        private Transform _player;
        private Vector3 _positionOffset = new Vector3(1.5f, 0.5f, 0);
        private Quaternion _rotationOffset = Quaternion.Euler(0, 90, 0);
        private Quaternion _rotationAtChest = Quaternion.Euler(0, 0, 90);
        private float _maxDistance = 5f;

        private void Update()
        {
            Vector3 directionToChest = _chest.position - transform.position;
            Quaternion rotationToChest = Quaternion.LookRotation(directionToChest);

            if (Vector3.Distance(transform.position, _chest.position) > _maxDistance)
            {
                transform.position = _player.position + _positionOffset;
                transform.rotation = _rotationOffset * rotationToChest;
            }
            else
            {
                transform.position = _chest.position + Vector3.up;
                transform.rotation = _rotationAtChest;
            }
        }

        public void Init(Transform player) 
        { 
            _player = player;
        }
    }
}
