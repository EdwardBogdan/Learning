using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyProject.Components
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] string _sceneName;

        public void OnLoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}

