using UnityEngine;

namespace MyProject.Characters
{
    public class PlayerAnimationController : GeneralAnimationController
    {
        [SerializeField] [Range(1, 3)] int _countAttackAnimations;
        [SerializeField] RuntimeAnimatorController _disarmedAnimator;
        [SerializeField] RuntimeAnimatorController _armedAnimator;

        int _attackIndex;
        bool isArmed;

        public void SetArming(bool _value)
        {
            isArmed = _value;
            _animator.runtimeAnimatorController = _value ? _armedAnimator : _disarmedAnimator;
            Data.GameSession.CurrentSession._isArmed = _value;
        }
        public override void SetTriggerAttack()
        {
            if (!isArmed || _isAttacking) return;

            _animator.SetTrigger(animatorKey_Attack);

            StartCoroutine(AttackDelay());

            _attackIndex = Random.Range(1, _countAttackAnimations + 1);
            _animator.SetInteger(animatorKey_AttackIndex, _attackIndex);
        }
        public override void SetTriggerSpecialAttack()
        {
            if (isArmed)
            {
                _animator.SetTrigger(animatorKey_SpecialAttack);
            }
        }
    }
}
