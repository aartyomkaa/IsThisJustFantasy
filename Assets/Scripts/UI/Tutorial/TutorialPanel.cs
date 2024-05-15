using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class TutorialPanel : Screen
    {
        private void OnEnable()
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * -Vector3.back,
                  Camera.main.transform.rotation * Vector3.up);
        }
    }
}
