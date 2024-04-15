using UnityEngine;

namespace Assets.Scripts.UI
{
    internal abstract class Screen : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup CanvasGroup;
        [SerializeField] protected AudioSource ButtonAudio;

        private void Start()
        {
            Close();
        }

        public void Close()
        {
            CanvasGroup.alpha = 0f;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }

        public virtual void Open()
        {
            CanvasGroup.alpha = 1f;
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
        }
    }
}
