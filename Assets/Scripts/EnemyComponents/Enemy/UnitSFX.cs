using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    internal class UnitSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip _attack;
        [SerializeField] private AudioClip _dead;
        [SerializeField] private AudioClip _walk;
        [SerializeField] private AudioSource _source;

        public void PlayDeathSound()
        {
            _source.clip = _dead;
            _source.Play();
        }

        public void PlayAttackSound()
        {
            _source.clip = _attack;
            _source.Play();
        }

        public void PlayWalkSound()
        {
            _source.clip = _walk;
            _source.Play();
        }
    }
}
