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
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new(transform.position.x, transform.position.y + 1, transform.position.z), new(0.3f, 0.3f, 0));
        }
    }
}
