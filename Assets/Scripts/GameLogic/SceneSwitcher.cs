using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal class SceneSwitcher : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
