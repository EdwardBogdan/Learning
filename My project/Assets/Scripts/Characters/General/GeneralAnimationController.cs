using System.Collections;
using UnityEngine;

namespace MyProject.Characters
{
    public class GeneralAnimationController : MonoBehaviour
    {
        [SerializeField] protected float _attackDelay;

        protected Animator _animator;

        protected bool _isAttacking = false;
        protected bool _attackPriority = false;

        protected static readonly int animatorKey_IsGrounded = Animator.StringToHash("IsGrounded");
        protected static readonly int animatorKey_IsRunning = Animator.StringToHash("IsRunning");
        protected static readonly int animatorKey_IsJumping = Animator.StringToHash("IsJumping");
        protected static readonly int animatorKey_IsAttacking = Animator.StringToHash("IsAttacking");
        protected static readonly int animatorKey_Hit = Animator.StringToHash("Take Hit");
        protected static readonly int animatorKey_DeathHit = Animator.StringToHash("Dead Hit");
        protected static readonly int animatorKey_Attack = Animator.StringToHash("Attack");
        protected static readonly int animatorKey_SpecialAttack = Animator.StringToHash("Special Attack");
        protected static readonly int animatorKey_AttackIndex = Animator.StringToHash("AttackIndex");
        protected static readonly int animatorKey_VerticalVelocity = Animator.StringToHash("Vertical Velocity");
        protected static readonly int animatorKey_AttackPriority = Animator.StringToHash("Attack Priority");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void SetIsGrounded(bool _value)
        {
            _animator.SetBool(animatorKey_IsGrounded, _value);
        }
        public void SetIsRunning(bool _value)
        {
            _animator.SetBool(animatorKey_IsRunning, _value);
        }
        public void SetIsJumping(bool _value)
        {
            _animator.SetBool(animatorKey_IsJumping, _value);
        }
        public void SetTriggerHit()
        {
            _animator.SetTrigger(animatorKey_Hit);
        }
        public virtual void SetTriggerAttack()
        {
            if (_isAttacking) return;

            _animator.SetTrigger(animatorKey_Attack);
            StartCoroutine(AttackDelay());
        }
        public virtual void SetTriggerSpecialAttack()
        {
            _animator.SetTrigger(animatorKey_SpecialAttack);
        }
        public void SetTriggerDeathHit()
        {
            _animator.SetTrigger(animatorKey_DeathHit);
        }
        public void SetFloatVerticalVelocity(float _value)
        {
            _animator.SetFloat(animatorKey_VerticalVelocity, _value);
        }

        protected IEnumerator AttackDelay()
        {
            _isAttacking = _attackPriority = true;
            _animator.SetBool(animatorKey_AttackPriority, _attackPriority);
            yield return new WaitForEndOfFrame();
            _animator.SetBool(animatorKey_IsAttacking, _isAttacking);
            yield return new WaitForSeconds(_attackDelay);
            _isAttacking = _attackPriority = false;
            _animator.SetBool(animatorKey_AttackPriority, _attackPriority);
            _animator.SetBool(animatorKey_IsAttacking, _isAttacking);
            Debug.Log("Can attack");
        }
    }
}

