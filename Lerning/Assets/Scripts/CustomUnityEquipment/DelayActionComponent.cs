using UnityEngine;
using CustomUnityUtils.TimeUtils;
using UnityEngine.Events;

namespace CustomUnityEquipment
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
