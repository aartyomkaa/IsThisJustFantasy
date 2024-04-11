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
        [SerializeField] private float _valueOfScaleOfParticleOfDestroy;
        [SerializeField] private Transform _spotOfDestroyEffects;

        private AudioSource _audiosourse;
        private Vector3 _scaleOfParticleOfDestroy;

        public int IndexOfBuilding; 
        private float _startStrength;

        public Transform Transform => transform;

        public Action<Transform> Destroyed;

        private void Awake()
        {
            _audiosourse = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if(_audiosourse.clip != null)
            {
                _audiosourse.Play();
            }
           
            _startStrength = _strength;
            SetScaleOfParticleOfDestroy();
        }

        private void SetScaleOfParticleOfDestroy()
        {
            _scaleOfParticleOfDestroy = new Vector3(_valueOfScaleOfParticleOfDestroy, _valueOfScaleOfParticleOfDestroy, _valueOfScaleOfParticleOfDestroy);
            _particleOfDestroy.transform.localScale = _scaleOfParticleOfDestroy;
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
            Instantiate(_particleOfDestroy, _spotOfDestroyEffects.position, Quaternion.identity);
            Destroyed?.Invoke(transform.parent);
            gameObject.SetActive(false);
            RefreshStrength();    
        }
    }
}