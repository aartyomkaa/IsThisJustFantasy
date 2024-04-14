using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameLogic
{
    internal class SceneLoader : MonoBehaviour
    {
        private void OnEnable()
        {
            LoadMenuScene();
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadSceneAsync(SceneNames.Menu, LoadSceneMode.Single);
        }
    }
}
