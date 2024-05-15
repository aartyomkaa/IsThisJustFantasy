using UnityEngine;
using Assets.Scripts.GameLogic.Interfaces;

namespace Assets.Scripts.PlayerComponents.Weapons
{
    internal class Mark : MonoBehaviour
    {
        private float _offset = 2.5f;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void MarkEnemy(IDamageable enemy)
        {
            gameObject.SetActive(true);
            transform.position = enemy.Transform.position + Vector3.up * _offset;
            transform.LookAt(transform.position + Camera.main.transform.rotation * -Vector3.back,
                      Camera.main.transform.rotation * Vector3.up);
        }

        public void UnMarkEnemy()
        {
            gameObject.SetActive(false);
        }
    }
}