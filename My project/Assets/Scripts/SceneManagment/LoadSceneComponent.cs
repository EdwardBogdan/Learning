using MyProject.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyProject.Components
{
    public class LoadSceneComponent : MonoBehaviour
    {
        [SerializeField] string _sceneName;

        public void LoadScene()
        {
            GameSession.CurrentSession.SaveDataFromSession();

            SceneManager.LoadScene(_sceneName);
        }
    }
}

