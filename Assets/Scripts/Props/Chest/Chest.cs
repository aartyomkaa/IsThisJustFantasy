using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic.Damageable;
using Assets.Scripts.PlayerComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Props.Chest
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    internal class Chest : MonoBehaviour
    {
        [SerializeField] private int _coins;
        [SerializeField] ParticleSystem _particleOfGiveCoins;
        
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
                GiveCoins(player);
            }
        }

        private void GiveCoins(Player player)
        {
            _isEmpty = true;
            player.Wallet.AddCoins(_coins);
        }

        public void SetCountOfCoins(int coins)
        {
            _coins = coins;
        }
    }
}