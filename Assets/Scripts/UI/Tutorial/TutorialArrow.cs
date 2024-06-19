using UnityEngine;
using Assets.Scripts.PlayerComponents;
using System;

namespace Assets.Scripts.UI
{
    internal class TutorialArrow : MonoBehaviour
    {
        public event Action Collected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                gameObject.SetActive(false);
                Collected?.Invoke();
            }
        }
    }
}
