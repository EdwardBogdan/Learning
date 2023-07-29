using UnityEngine;

namespace MyProject.Components
{
    public class DrawSphere : MonoBehaviour
    {
        [SerializeField] float _radius;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}
