using ScriptAnimation;
using CustomUnityUtils.TimeUtils;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Totem
{
    public class TotemBase : MonoBehaviour
    {
        [SerializeField] private string _attackClipName;
        [SerializeField] private ScriptSmartAnimator _animator;
        [SerializeField] private Cooldown _delay;

        [SerializeField] private UnityEvent _OnBeforeAttack;

        private Coroutine _routine;

        public void OnCast(GameObject _object)
        {
            if (!_object.TryGetComponent<TotemBase>(out var totem)) return;

            totem.AttackTrigger();
        }
        public void AttackTrigger()
        {
            if (_routine != null) return;

            _delay.Reset();
            _routine = StartCoroutine(Delayer());
        }

        private IEnumerator Delayer()
        {
            while (!_delay.IsReady)
            {
                yield return new WaitForEndOfFrame();
            }

            _OnBeforeAttack?.Invoke();
            _animator.SetClip(_attackClipName);
            _routine = null;
        }
    }
}
