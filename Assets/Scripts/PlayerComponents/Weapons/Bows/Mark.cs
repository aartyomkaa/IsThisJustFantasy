using UnityEngine;
using Assets.Scripts.GameLogic.Interfaces;

namespace Assets.Scripts.PlayerComponents.Weapons
{
    internal class Mark : MonoBehaviour
    {
        private IDamageable _target;

        public Transform Target => _target.Transform;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void MarkEnemy(IDamageable enemy)
        {
            _target = enemy;
            gameObject.SetActive(true);
            transform.position = enemy.Transform.position + Vector3.up * 2.5f;
            transform.LookAt(transform.position + Camera.main.transform.rotation * -Vector3.back,
                      Camera.main.transform.rotation * Vector3.up);
        }

        public void UnMarkEnemy()
        {
            gameObject.SetActive(false);
        }
    }
}