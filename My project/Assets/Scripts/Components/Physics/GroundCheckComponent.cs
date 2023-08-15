using UnityEngine;

namespace MyProject.Components
{
    public class GroundCheckComponent : MonoBehaviour
    {
        public bool isGrounded { get; private set; }
        public void IsGrounded(GameObject hit)
        {
            isGrounded = hit != null;
        }
    }
}
