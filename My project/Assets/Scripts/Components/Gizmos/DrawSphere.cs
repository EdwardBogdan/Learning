using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class DrawSphere : MonoBehaviour
    {
        [SerializeField] float _radius;

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransporentColorRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}
