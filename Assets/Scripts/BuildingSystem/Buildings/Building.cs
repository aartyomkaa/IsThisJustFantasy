using System;
using UnityEngine;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.UI;


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
        [SerializeField] private TutorialPanel _tutorialPanel;

        private AudioSource _audiosourse;
        private Vector3 _scaleOfParticleOfDestroy;
        protected ColliderPanelEventer Eventer;

        public int IndexOfBuilding; 
        private float _startStrength;

        public Transform Transform => transform;

        public event Action<Transform> Destroyed;
        public event Action<ColliderPanelEventer> BuildWithEventorWasMade;

        private void Awake()
        {
            _audiosourse = GetComponent<AudioSource>();
            Eventer = GetComponentInChildren<ColliderPanelEventer>();
        }

        private void OnEnable()
        {
            if (_audiosourse.clip != null)
            {
                _audiosourse.Play();
            }
        }


        private void Start()
        {   
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

        public void AnnounceOfCreation()
        {
            if(Eventer != null)
            {
                BuildWithEventorWasMade?.Invoke(Eventer);
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