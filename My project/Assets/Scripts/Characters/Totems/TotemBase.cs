using MyProject.Components.Cast;
using MyProject.Components.ScriptAnimations;
using MyProject.Utils;
using System.Collections;
using UnityEngine;

namespace MyProject.Characters.Totem
{
    public class TotemBase : MonoBehaviour
    {
        [SerializeField] private string _attackClipName;
        [SerializeField] private CastComponent _castNext;
        [SerializeField] private ScriptSmartAnimator _animator;
        [SerializeField] private Cooldown _delay;

        public ScriptSmartAnimator Animator => _animator;

        public void OnCast(GameObject _object)
        {
            if (!_object.TryGetComponent<TotemBase>(out var totem)) return;

            totem.AttackTrigger();
        }
        public void AttackTrigger()
        {
            _delay.Reset();
            StartCoroutine(Delayer());
        }

        private IEnumerator Delayer()
        {
            while (!_delay.IsReady)
            {
                yield return new WaitForEndOfFrame();
            }

            _animator.SetClip(_attackClipName);
            _castNext.Cast();
        }
    }
}
