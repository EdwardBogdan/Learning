using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyProject.Components
{
    public class LoadSceneComponent : MonoBehaviour
    {
        [SerializeField] string _sceneName;

        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}

