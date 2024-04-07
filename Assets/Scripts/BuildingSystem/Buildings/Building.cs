using System;
using UnityEngine;
using Assets.Scripts.GameLogic.Damageable;
using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.BuildingSystem
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]

    internal abstract class Building : MonoBehaviour, IDamageable
    {
        [SerializeField] private ParticleSystem _particleOfDestroy;

        public int IndexOfBuilding;

        public float Strength;
        
        public AudioSource _audiosourseOfCreation;
        
        public Transform Transform => transform;

        public static Action<Transform> Destroyed;

        private void Awake()
        {
            _audiosourseOfCreation = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audiosourseOfCreation.Play();
        }

        public void TakeDamage(float damage)
        {
            if (Strength > 0 && damage > 0)
            {
                Strength -= damage;

                if (Strength <= 0)
                {
                    Destroy();
                }
            }
        }

        protected void Destroy()
        {
            //Instantiate(EffectOfDestroying, transform.position, Quaternion.identity);
            Destroyed?.Invoke(this.transform);
            DestroyImmediate(gameObject);
            _particleOfDestroy.Play();
        }
    }
}