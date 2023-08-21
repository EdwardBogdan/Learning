using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class AwakeComponent : MonoBehaviour
    {
        [SerializeField] UnityEvent _action;

        private void Awake()
        {
            _action?.Invoke();
            Destroy(this);
        }
    }
}
