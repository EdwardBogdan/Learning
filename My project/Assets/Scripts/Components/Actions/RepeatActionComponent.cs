using UnityEngine;
using UnityEngine.Events;
using MyProject.Utils;

namespace MyProject.Components.ActionComponents
{
    public class RepeatActionComponent : MonoBehaviour , INaming
    {
        [SerializeField] Cooldown _delay;
        [SerializeField] UnityEvent _action;

        public string NameElement => "Repeat";

        private void Start()
        {
            _delay.Reset();
        }
        private void FixedUpdate()
        {
            if (_delay.IsReady)
            {
                _action?.Invoke();
                _delay.Reset();
            }
        }
    }
}
