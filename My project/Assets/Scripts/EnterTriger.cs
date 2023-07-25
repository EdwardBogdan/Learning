using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class EnterTriger : MonoBehaviour
    {
        [SerializeField] UnityEvent _action;
        [SerializeField] string[] _tags;

        private void OnTriggerEnter2D(Collider2D other)
        {
            foreach (string tag in _tags)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    _action?.Invoke();
                    break;
                }
            }
            
        }
    }
}
