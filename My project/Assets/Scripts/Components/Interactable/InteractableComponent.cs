using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components.Interactable
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] UnityEvent _action;
        public void Interact()
        {
            _action?.Invoke();
        }
    }
}
