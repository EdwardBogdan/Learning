
using UnityEngine;

namespace MyProject.Components
{
    public class EnterTrigerPassComponent : MonoBehaviour
    {
        [SerializeField] UnityEvent_GameObject _action;
        [SerializeField] string[] _tags;

        private void OnTriggerEnter2D(Collider2D other)
        {
            foreach (string tag in _tags)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    _action?.Invoke(other.gameObject);
                    break;
                }
            }
        }
    }
}
