using MyProject.Components.Cast;
using MyProject.Components.ScriptAnimations;
using UnityEngine;

namespace MyProject.Characters.Totem
{
    public class TotemBase : MonoBehaviour
    {
        protected CastComponent _castNext;
        protected ScriptSmartAnimator _animator;

        private void Awake()
        {
            _castNext = GetComponent<CastComponent>();
            _animator = GetComponent<ScriptSmartAnimator>();
        }
        public void Cast()
        {
            _castNext.Cast();
        }
        public void OnCast(GameObject _object)
        {
            if (!_object.TryGetComponent<TotemBase>(out var totem)) return;

            totem.Action();
        }
        public virtual void Action()
        {
            Cast();
        }
    }
}
