using UnityEngine;
using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.UI
{
    internal class TutorialArrow : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
