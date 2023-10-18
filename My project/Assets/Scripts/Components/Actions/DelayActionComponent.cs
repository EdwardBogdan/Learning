using UnityEngine;
using MyProject.Utils;
using UnityEngine.Events;

namespace MyProject.Components.ActionComponents
{
    public class DelayActionComponent : MonoBehaviour
    {
        [SerializeField] Cooldown _delay;
        [SerializeField] UnityEvent _action;
        private void Start()
        {
            _delay.Reset();
        }
        private void FixedUpdate()
        {
            if (_delay.IsReady)
            {
                _action?.Invoke();
                Destroy(this);
            }
        }
    }
}
