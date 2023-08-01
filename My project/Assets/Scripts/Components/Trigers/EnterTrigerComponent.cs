using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class EnterTrigerComponent : MonoBehaviour
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
