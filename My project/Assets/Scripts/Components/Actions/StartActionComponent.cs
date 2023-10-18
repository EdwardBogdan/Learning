using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components.ActionComponents
{
    public class StartActionComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;
        private void Start()
        {
            _action?.Invoke();
        }
    }
}
