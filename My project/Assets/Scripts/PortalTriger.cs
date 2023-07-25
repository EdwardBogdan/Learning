using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Components
{
    public class PortalTriger : MonoBehaviour
    {
        [SerializeField] string[] _tags;
        [SerializeField] Transform _exit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            foreach (string tag in _tags)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    other.transform.position = _exit.position;
                    break;
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (_exit != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _exit.position);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position, new(0.3f,0.3f,0));
            }
            
        }
    }
}