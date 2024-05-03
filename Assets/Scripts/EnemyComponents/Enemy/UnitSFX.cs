using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class UnitSFX : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _attack;
        [SerializeField] private List<AudioClip> _death;
        [SerializeField] private List<AudioClip> _walk;
        [SerializeField] private AudioSource _source;

        private float _minPitch = 0.8f;
        private float _maxPitch = 1.5f;
        private System.Random _random = new System.Random();

        public void PlaySound(List<AudioClip> clipList, bool loop = false)
        {
            _source.Stop();
            _source.loop = loop;
            _source.clip = GetRandomClip(clipList);
            _source.pitch = UnityEngine.Random.Range(_minPitch, _maxPitch);
            _source.Play();
        }

        public void Stop()
        {
            _source.Stop();
        }

        public void PlayDeathSound() => PlaySound(_death);

        public void PlayAttackSound() => PlaySound(_attack);

        public void PlayWalkSound() => PlaySound(_walk, true);

        private AudioClip GetRandomClip(List<AudioClip> clipList)
        {
            if (clipList.Count == 0)
                throw new ArgumentException("There are no AudioClips!");

            return clipList[_random.Next(clipList.Count)];
        }
    }
}