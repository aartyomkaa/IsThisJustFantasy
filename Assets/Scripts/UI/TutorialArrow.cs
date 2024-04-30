using Assets.Scripts.PlayerComponents;
using UnityEngine;

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
