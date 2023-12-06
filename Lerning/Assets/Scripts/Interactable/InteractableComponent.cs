using UnityEngine;
using UnityEngine.Events;

namespace Interactable
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] bool _canBeUsed;
        [SerializeField] UnityEvent _action;
        public void Interact()
        {
            if (_canBeUsed) _action?.Invoke();
        }
        public void SetStatus(bool value)
        {
            _canBeUsed = value;
        }
    }
}
