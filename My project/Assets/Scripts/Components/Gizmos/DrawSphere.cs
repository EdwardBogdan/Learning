using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class DrawSphere : MonoBehaviour
    {
        [SerializeField] float _radius;
        [SerializeField] TypeColor _color;

        private void OnDrawGizmosSelected()
        {
            Handles.color = ColorStore.GetColor(_color);
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}
