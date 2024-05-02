using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    internal abstract class Screen : MonoBehaviour
    {
       // [SerializeField] protected AudioSource ButtonAudio;
       [SerializeField] private CanvasGroup _canvasGroup;

        private void Start()
        {
            Close(); 
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
    }
}
