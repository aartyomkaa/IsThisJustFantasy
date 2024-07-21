using System;
using UnityEngine;

namespace Assets.Scripts.PlayerUnits
{
    internal class Selectable : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _ring;

        private bool _isSelected;
        private float _offset = 0.1f;

        public event Action<Selectable> Selected;
       
        public event Action<Selectable> Deselected;

        private void Awake()
        {
            Vector3 offset = new Vector3(transform.position.x, transform.position.y + _offset, transform.position.z);

            _ring = Instantiate(_ring, offset, _ring.transform.rotation, transform);

            _ring.Stop();
            _isSelected = false;
        }

        private void OnMouseDown()
        {
            if (_isSelected)
            {
                _isSelected = false;
                _ring.Stop();
                Deselected?.Invoke(this);
            }
            else
            {
                _isSelected = true;
                _ring.Play();
                Selected?.Invoke(this);
            }
        }

        private void OnDisable()
        {
            _isSelected = false;
            _ring.Stop();
            Deselected?.Invoke(this);
        }
    }
}
