using UnityEngine;

namespace Assets.Scripts.UI
{

    internal abstract class Screen : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup CurrentCanvasGroup;
       // [SerializeField] protected AudioSource ButtonAudio;

       
        
        private void Start()
        {
            Close(); 
        }

        public void Close()
        {
            CurrentCanvasGroup.alpha = 0f;
            CurrentCanvasGroup.interactable = false;
            CurrentCanvasGroup.blocksRaycasts = false;
        }

        public virtual void Open()
        {
            CurrentCanvasGroup.alpha = 1f;
            CurrentCanvasGroup.interactable = true;
            CurrentCanvasGroup.blocksRaycasts = true;
        }
    }
}
