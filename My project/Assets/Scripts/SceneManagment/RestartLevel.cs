using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyProject.Components
{
    public class RestartLevel : MonoBehaviour
    {
        public void Restart()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
