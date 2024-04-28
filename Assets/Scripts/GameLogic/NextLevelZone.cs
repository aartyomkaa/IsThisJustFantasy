using Assets.Scripts.PlayerComponents;
using System;
using UnityEngine;
using Assets.Scripts.Constants;

namespace Assets.Scripts
{
    public class NextLevelZone : MonoBehaviour
    {
        private bool _hasLeveledUp = false;

        public event Action PlayerWentIn;
        public event Action PlayerWentOut;
        public event Action LevelUped;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                PlayerWentIn?.Invoke();

                if (_hasLeveledUp == false)
                {
                    player.LevelUp();
                    LevelUped?.Invoke();
                    _hasLeveledUp = true;
                    PlayerPrefs.SetInt(PlayerConfigs.HasPassedTutorial, 1);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                PlayerWentOut?.Invoke();
            }
        }
    }
}

