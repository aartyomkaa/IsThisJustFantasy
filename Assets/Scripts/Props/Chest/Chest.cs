using UnityEngine;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.Props.Chest
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    internal class Chest : MonoBehaviour
    {
        [SerializeField] private int _coins;
        [SerializeField] private ParticleSystem _particleOfGiveCoins;
        [SerializeField] private ParticleSystem _particleOfPosition;

        private Animator _animator;
        private AudioSource _audiosourse;
        private bool _isEmpty = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audiosourse = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player) && _isEmpty == false)
            {
                _animator.SetBool(AnimatorHash.IsPlayerNear, true);
                Instantiate(_particleOfGiveCoins, transform.position, Quaternion.identity);
                _audiosourse.Play();
                GiveCoinsToPlayer(player);
                _particleOfPosition.Stop();
            }
        }

        public void SetCountOfCoins(int coins)
        {
            _coins = coins;
        }

        private void GiveCoinsToPlayer(Player player)
        {
            _isEmpty = true;
            player.Wallet.AddCoins(_coins);
        }
    }
}