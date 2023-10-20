using MyProject.Components.Triggers;
using MyProject.Utils;
using UnityEngine;

namespace MyProject.Physic.PAController
{
    public class PlayerAnimatorController : BaseAnimatorController
    {
        [SerializeField][Range(1, 3)] int _attacks;
        [SerializeField] RuntimeAnimatorController _disarmedAnimator;
        [SerializeField] RuntimeAnimatorController _armedAnimator;

        private bool isArmed;
        private int _attackIndex;
        private TouchComponent _wall;
        private bool IsWalled => _wall.IsTouched;

        protected static readonly int animatorKey_AttackIndex = Animator.StringToHash("Attack Index");

        private static readonly int animatorKey_IsWalled = Animator.StringToHash("IsWalled");
        protected override void Start()
        {
            base.Start();
            _wall = _data.wallData;
        }
        protected override void FixedUpdate()
        {
            _controller.SetBool(animatorKey_IsWalled, IsWalled);
            base.FixedUpdate();
        }
        public void SetArming(bool _value)
        {
            isArmed = _value;
            _controller.runtimeAnimatorController = _value ? _armedAnimator : _disarmedAnimator;
        }
        public override void ACTriggerAttack()
        {
            if (!_attackInterval.IsReady || !isArmed) return;

            _attackInterval.Reset();

            _controller.SetTrigger(animatorKey_Attack);

            _attackIndex = Random.Range(1, _attacks + 1);
            _controller.SetInteger(animatorKey_AttackIndex, _attackIndex);
        }
        public override void ACTriggerThrow()
        {
            if (!_throwInterval.IsReady || !isArmed) return;

            _controller.SetTrigger(animatorKey_Throw);
        }
    }
}