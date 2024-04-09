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
        [SerializeField] private float _strength;

        public int IndexOfBuilding;
       
        private float _startStrength;
      

        
        
        public AudioSource _audiosourseOfCreation;
        
        public Transform Transform => transform;

        public Action<Transform> Destroyed;

        private void Awake()
        {
            _audiosourseOfCreation = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audiosourseOfCreation.Play();
            _startStrength = _strength;
        }

        private void RefreshStrength()
        {
            _strength = _startStrength;
        }

        public void TakeDamage(float damage)
        {
            if (_strength > 0 && damage > 0)
            {
                _strength -= damage;

                if (_strength <= 0)
                {
                    Destroy();       
                }
            }
        }

       

        protected void Destroy()
        {
            //Instantiate(EffectOfDestroying, transform.position, Quaternion.identity);
            Destroyed?.Invoke(transform.parent);
            
            //DestroyImmediate(gameObject);
            gameObject.SetActive(false);
            RefreshStrength();
            //_particleOfDestroy.Play();
        }
    }
}